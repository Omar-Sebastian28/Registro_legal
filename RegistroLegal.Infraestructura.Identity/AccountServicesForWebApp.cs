using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Aplications.Dto.Email;
using RegistroLegal.Core.Aplications.Dto.UserDto;
using RegistroLegal.Core.Aplications.Interfaces;
using RegistroLegal.Infraestructura.Identity.Entities;

namespace RegistroLegal.Infraestructura.Identity
{
    public class AccountServicesForWebApp : IAccountServicesForWebApp
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly IEmailServices _emailServices;


        public AccountServicesForWebApp(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager, IEmailServices emailServices)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _emailServices = emailServices;
        }

        //Autenticar la cuenta.
        public async Task<LoginReponseDto> AuthenticateAsync(LoginDto loginDto)
        {
            LoginReponseDto response = new()
            {
                Id = "",
                Nombre = "",
                Apellido = "",
                Email = "",
                UserName = "",
                Role = "",
                HasError = false,
            };

            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user is null)
            {
                response.HasError = true;
                response.Error = $"No se encontró ninguna cuenta asociada al nombre de usuario ingresado. Verifica que esté escrito correctamente o intenta con tu correo electrónico. Si el problema persiste, puedes crear una nueva cuenta o contactar al soporte.";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Tu cuenta aún no ha sido activada. Por favor, revisa tu correo electrónico y sigue el enlace de confirmación para completar el registro..";
                return response;
            }
         
            var result = await _singInManager.PasswordSignInAsync(user.UserName ?? "", loginDto.Password, false, true);

            if (!result.Succeeded)
            {
                response.HasError = true;
                if (result.IsLockedOut)
                {
                    response.Error = "Has excedido el número máximo de intentos permitidos. Por seguridad, tu acceso ha sido bloqueado temporalmente. Podrás intentarlo nuevamente en 10 minutos.";
                }
                else 
                {
                    response.Error = "Las credenciales ingresadas no son váli1das. Verifica tu correo y contraseña, y vuelve a intentarlo.";
                }
                    return response;
            }
            
            var roles = await _userManager.GetRolesAsync(user);

            response.Id = user.Id;
            response.Nombre = user.Nombre;
            response.Apellido = user.Apellido;
            response.Email = user.Email ?? "";
            response.Phone = user.PhoneNumber;
            response.IsVerified = user.EmailConfirmed;
            response.Roles = roles.ToList();

            return response;
        }


        //Cerrar la sesion.
        public async Task SingOutAsync()
        {
            await _singInManager.SignOutAsync();
        }


        //Eliminar un usuario.
        public async Task<DeleteResponseDto> DeleteAsync(string userId)
        {
            DeleteResponseDto response = new()
            {
                HasError = false
            };

            var entity = await _userManager.FindByIdAsync(userId);
            if (entity is not null)
            {
                var resultDelete = await _userManager.DeleteAsync(entity);
                if (!resultDelete.Succeeded)
                {
                    response.HasError = true;
                    response.Error = $"Ocurrio un error al borrar el usuario. {entity.UserName}";
                    return response;
                }
            }

            response.HasError = false;
            return response;
        }


        //Reseteo de contraseña.
        public async Task<ResetPasswordResponseDto> ForgotPassword(ResetPasswordResponseDto dto)
        {
            ResetPasswordResponseDto response = new()
            {
                UserName = "",
                HasError = false
            };

            var entity = await _userManager.FindByNameAsync(dto.UserName);

            if (entity is not null)
            {
                entity.EmailConfirmed = false;
                var resetUri = await VerificacionPasswordlUri(entity, dto.Origin ?? "");
               
                await _emailServices.SendAsync(new EmailRequestDto()
                {
                    To = entity.Email,
                    Subject = "Solicitud de restablecimiento de contraseña",
                    HtmlBdy = $@"
                        <html>
                            <head>
                                <style>
                                    body {{ font-family: Arial, sans-serif; color: #333; }}
                                    .container {{ max-width: 600px; margin: auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px; background-color: #f9f9f9; }}
                                    .button {{ display: inline-block; padding: 10px 20px; background-color: #007bff; color: white; text-decoration: none; border-radius: 4px; }}
                                    .footer {{ margin-top: 20px; font-size: 0.9em; color: #777; }}
                                </style>
                            </head>
                            <body>
                                <div class='container'>
                                    <h2>Hola,</h2>
                                    <p>Recibimos una solicitud para restablecer tu contraseña. Si fuiste tú, puedes hacerlo haciendo clic en el siguiente botón:</p>
                                    <p><a href='{resetUri}' class='button'>Restablecer contraseña</a></p>
                                    <p>Si no solicitaste este cambio, puedes ignorar este mensaje. Tu cuenta seguirá segura.</p>
                                    <div class='footer'>
                                        <p>Este mensaje fue generado automáticamente. Por favor, no respondas a este correo.</p>
                                    </div>
                                </div>
                            </body>
                         </html>"
                });
                await _userManager.UpdateAsync(entity);
                return response;
            }

            response.HasError = true;
            response.Error = $"No encontramos ningún usuario registrado con el usuario '{dto.UserName}'";
            return response;
        }


        //Confirmacion de contraseña.
        public async Task<ResetPasswordResponseDto> ConfirmForgotPassword(ResetPasswordRequestDto dto)
        {
            ResetPasswordResponseDto response = new()
            {
                UserName = "",
                HasError = false
            };

            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user is not null)
            {
                var tokenUser = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));
                var resultPassword = await _userManager.ResetPasswordAsync(user, tokenUser, dto.Password);

                if (!resultPassword.Succeeded)
                {
                    response.HasError = true;
                    response.Error = "Ocurrio un error al cambiar contraseña.";
                    return response;
                }

                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return response;
            }

            response.HasError = true;
            response.Error = $"No hubo ninguna coincidencia con el usuario que ha especificado.";
            return response;
        }



        //Buscamos todos los usuarios con email confirmado y los no confirmados por igual.
        public async Task<List<DtoUser>> GetAllUser(bool? isActive = true)
        {

            List<DtoUser> listUserDto = [];
            var usersQuery = _userManager.Users;

            if (isActive != null && isActive == true)
            {
                usersQuery = usersQuery.Where(u => u.EmailConfirmed == true);
            }
            else
            {
                usersQuery = usersQuery.Where(u => !u.EmailConfirmed);
            }

            var listUser = await usersQuery.ToListAsync();

            foreach (var item in listUser)
            {
                var roleList = await _userManager.GetRolesAsync(item);

                listUserDto.Add(new DtoUser()
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Apellido = item.Apellido,
                    Email = item.Email ?? "",
                    UserName = item.UserName ?? "",
                    ImagenPerfil = item.ImagenPerfil,
                    Phone = item.PhoneNumber,
                    IsVerified = item.EmailConfirmed,
                    Role = roleList.FirstOrDefault() ?? ""
                });
            }

            return listUserDto;
        }


        //Registro para el user.
        public async Task<ResponseDto> RegisterAsync(SaveUserDto saveUserDto, string origin)
        {
            ResponseDto response = new()
            {
                Id = "",
                Nombre = "",
                Apellido = "",
                Email = "",
                UserName = "",
                Password = "",
                HasError = false,
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(saveUserDto.UserName);

            if (userWithSameUserName is not null)
            {
                response.HasError = true;
                response.Error = $"'{saveUserDto.UserName}' esta en uso.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(saveUserDto.Email);

            if (userWithSameEmail is not null)
            {
                response.HasError = true;
                response.Error = $"El email que usted regitro, esta siendo utilizado.";
                return response;
            }

            AppUser user = new()
            {
                Nombre = saveUserDto.Nombre,
                Apellido = saveUserDto.Apellido,
                UserName = saveUserDto.UserName,
                Email = saveUserDto.Email,
                EmailConfirmed = false,
                ImagenPerfil = saveUserDto.ImagenPerfil
            };

            try
            {
                var result = await _userManager.CreateAsync(user, saveUserDto.Password);

                if (!result.Succeeded)
                {
                    response.HasError = true;

                    var error = result.Errors.Select(e => $"{e.Description}");
                    var mensajePersonalizado = "No se pudo crear el usuario. Verifica lo siguiente:\n\n" + string.Join("\n", error);   
                    
                    response.Error = mensajePersonalizado;
                    return response;
                }
                     var getVerificationUri = await VerificacionEmailUri(user, origin);
                    await _userManager.AddToRoleAsync(user, saveUserDto.Role);
                    await _emailServices.SendAsync(new EmailRequestDto()
                    {
                        To = saveUserDto.Email,
                        Subject = "Confirma tu cuenta en el sistema",
                        HtmlBdy = $@"
                                    <div style='font-family:Segoe UI, sans-serif; background-color:#f9f9f9; padding:40px;'>
                                        <div style='max-width:600px; margin:0 auto; background:white; border-radius:8px; box-shadow:0 4px 12px rgba(0,0,0,0.1); overflow:hidden;'>
        
                                            <!-- Encabezado -->
                                            <div style='background:#28a745; color:white; text-align:center; padding:20px;'>
                                                <h2 style='margin:0; font-size:22px;'>Bienvenido al Sistema</h2>
                                            </div>

                                            <!-- Cuerpo -->
                                            <div style='padding:30px; color:#333; font-size:16px; line-height:1.6;'>
                                                <p>Hola <strong>{saveUserDto.Nombre}</strong>,</p>
            
                                                <p>Gracias por registrarte en nuestro sistema. Para activar tu cuenta y comenzar a utilizar nuestros servicios, por favor confirma tu dirección de correo electrónico haciendo clic en el siguiente botón:</p>

                                                <p style='text-align:center; margin:30px 0;'>
                                                    <a href='{getVerificationUri}' 
                                                       style='background-color:#28a745; color:white; padding:12px 24px; font-size:16px; 
                                                              font-weight:bold; text-decoration:none; border-radius:6px; display:inline-block;'>
                                                        Confirmar cuenta
                                                    </a>
                                                </p>

                                                <p>Si no solicitaste este registro, simplemente puedes ignorar este mensaje.</p>

                                                <p style='margin-top:30px;'>Saludos cordiales,<br><strong>Equipo de Soporte</strong></p>
                                            </div>

                                            <!-- Footer -->
                                            <div style='background:#f1f1f1; color:#777; text-align:center; font-size:13px; padding:15px;'>
                                                © {DateTime.Now.Year} - Todos los derechos reservados<br/>
                                                Este es un correo automático, por favor no respondas directamente.
                                            </div>
                                        </div>
                                    </div>"
                    });
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }

                response.HasError = true;
                response.Error = ex.Message;
                return response;
            }

            var roles = await _userManager.GetRolesAsync(user);

            response.Id = user.Id;
            response.Nombre = user.Nombre;
            response.Apellido = user.Apellido;
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.ImagenPerfil = user.ImagenPerfil;
            response.Password = user.PasswordHash ?? "";
            response.Phone = user.PhoneNumber;
            response.Roles = roles.ToList();

            return response;
        }


        //Confirmamos la cuenta por email.
        public async Task<string> ConfirmAccount(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return "No se encontro el usuario. Por favor, registrese.";
            }

            var descodeToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var verificacionUser = await _userManager.ConfirmEmailAsync(user, descodeToken);

            if (!verificacionUser.Succeeded)
            {
                return $"El email: {user.Email} no se pudo confirmar correctamente.";
            }
            else
            {
                return "Gracias por confirmar su correo. ya puedes utilizar nuestra app.";
            }
        }



        //Editamos el usuario.
        public async Task<ResponseDto> EditUser(SaveUserDto saveUserDto, string origin, bool? creando = false)
        {
            bool modeCreacion = creando ?? false;

            ResponseDto response = new()
            {
                Id = "",
                Nombre = "",
                Apellido = "",
                Email = "",
                UserName = "",
                Password = "",
                HasError = false,
            };

            var user = await _userManager.FindByIdAsync(saveUserDto.Id);
            if (user is null)
            {
                response.HasError = true;
                response.Error = "Este usuario no esta registrado. verifique si los datos del usuario son correctos.";
                return response;
            }

            user.Nombre = saveUserDto.Nombre;
            user.Apellido = saveUserDto.Apellido;
            user.Email = saveUserDto.Email;
            user.PhoneNumber = saveUserDto.Phone;
            user.UserName = saveUserDto.UserName;
            if (!modeCreacion) 
            {
                user.EmailConfirmed = saveUserDto.Email == user.NormalizedEmail?.ToLower();
            }
            user.ImagenPerfil = string.IsNullOrWhiteSpace(saveUserDto.ImagenPerfil) ? user.ImagenPerfil : saveUserDto.ImagenPerfil;


            var userRoles = await _userManager.GetRolesAsync(user);
            var updateUser = await _userManager.UpdateAsync(user);

            if (updateUser.Succeeded)
            {   
                if (!userRoles.Contains(saveUserDto.Role))
                {
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRoleAsync(user, saveUserDto.Role);
                }

                if (!user.EmailConfirmed && !modeCreacion)
                {
                    var verificatioUri = await VerificacionEmailUri(user, origin);
                    await _emailServices.SendAsync(new EmailRequestDto()
                    {
                        To = saveUserDto.Email,
                        Subject = "Confirma tu cuenta",
                        HtmlBdy = $@"
                                <table width='100%' cellpadding='0' cellspacing='0' style='font-family:Segoe UI, sans-serif; background-color:#f9f9f9; padding:30px 0;'>
                                  <tr>
                                    <td align='center'>
                                      <table width='600' cellpadding='0' cellspacing='0' style='background:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 4px 12px rgba(0,0,0,0.1);'>
        
                                        <!-- Encabezado -->
                                        <tr>
                                          <td style='background:#28a745; padding:20px; text-align:center; color:#fff; font-size:22px; font-weight:bold;'>
                                            Confirmación de cuenta
                                          </td>
                                        </tr>

                                        <!-- Cuerpo -->
                                        <tr>
                                          <td style='padding:30px; color:#333; font-size:16px; line-height:1.6;'>
                                            <p>Hola <strong>{saveUserDto.Nombre}</strong>,</p>
                                            <p>Gracias por registrarte en nuestro sistema. Para completar el proceso y activar tu cuenta, haz clic en el siguiente botón seguro:</p>

                                            <p style='text-align:center; margin:30px 0;'>
                                              <a href='{verificatioUri}' 
                                                 style='background:#28a745; color:#ffffff; padding:14px 28px; text-decoration:none; 
                                                        border-radius:6px; font-weight:bold; font-size:16px; display:inline-block;'>
                                                 Confirmar cuenta
                                              </a>
                                            </p>

                                            <p>Si no solicitaste esta cuenta, simplemente ignora este mensaje.</p>
                                            <p style='margin-top:30px; font-size:14px; color:#777;'>
                                              Por tu seguridad, este enlace expirará en las próximas horas.
                                            </p>
                                          </td>
                                        </tr>

                                        <!-- Footer -->
                                        <tr>
                                          <td style='background:#f1f1f1; padding:15px; text-align:center; font-size:13px; color:#555;'>
                                            © {DateTime.UtcNow.Year} - Equipo de soporte<br>
                                            Este es un correo automático, por favor no respondas a este mensaje.
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                </table>"
                    });
                }

                if (!string.IsNullOrEmpty(saveUserDto.Password) && !modeCreacion)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, saveUserDto.Password);
                }
            }

            var updateRoles = await _userManager.GetRolesAsync(user);

            response.Id = user.Id;
            response.Nombre = user.Nombre;
            response.Apellido = user.Apellido;
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.ImagenPerfil = user.ImagenPerfil;
            response.Password = user.PasswordHash ?? "";
            response.Phone = user.PhoneNumber;
            response.Roles = updateRoles.ToList();

            return response;
        }


        //Buscamos el usuario por email.
        public async Task<DtoUser?> BuscarUsuarioPorEmail(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new DtoUser()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email ?? "",
                IsVerified = user.EmailConfirmed,
                ImagenPerfil = user.ImagenPerfil,
                Phone = user.PhoneNumber,
                UserName = user.UserName ?? "",
                Role = roles.FirstOrDefault() ?? ""
            };
            return userDto;
        }


        //Buscamos el usuario por id.
        public async Task<DtoUser?> BuscarUsuarioPorId(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user is null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new DtoUser()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email ?? "",
                IsVerified = user.EmailConfirmed,
                ImagenPerfil = user.ImagenPerfil,
                Phone = user.PhoneNumber,
                UserName = user.UserName ?? "",
                Role = roles.FirstOrDefault() ?? ""
            };

            return userDto;
        }


        //Buscamos el usuario por nombre de usuario.
        public async Task<DtoUser?> BuscarUsuarioPorUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new DtoUser()
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Email = user.Email ?? "",
                IsVerified = user.EmailConfirmed,
                ImagenPerfil = user.ImagenPerfil,
                Phone = user.PhoneNumber,
                UserName = user.UserName ?? "",
                Role = roles.FirstOrDefault() ?? ""
            };

            return userDto;
        }



        #region Metodos Privados
        private async Task<string> VerificacionEmailUri(AppUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "Login/ConfirmEmail";
            var completeUrl = new Uri(string.Concat(origin, "/", route));
            var verificationUri = QueryHelpers.AddQueryString(completeUrl.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri.ToString(), "token", token);

            return verificationUri;
        }


        private async Task<string> VerificacionPasswordlUri(AppUser user, string origin)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "Login/ResetPassword";
            var completeUrl = new Uri(string.Concat(origin, "/", route));
            var resetUri = QueryHelpers.AddQueryString(completeUrl.ToString(), "userId", user.Id);
            resetUri = QueryHelpers.AddQueryString(resetUri.ToString(), "token", token);

            return resetUri;
        }
        #endregion
    }
}
