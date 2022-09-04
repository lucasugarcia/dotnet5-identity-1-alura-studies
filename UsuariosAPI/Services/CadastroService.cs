using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;

        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Result> CadastrarUsuario(CreateUsuarioDto createDto)
        {
            var usuario = _mapper.Map<Usuario>(createDto);

            var usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, createDto.Password);

            if (resultadoIdentity.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity);

                var encodedCode = HttpUtility.UrlEncode(code);

                _emailService.EnviarEmail(new[] { usuarioIdentity.Email }, "Link de Ativação", usuarioIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess(code);
            }

            return Result.Fail("Houve uma falha ao cadastrar o usuário");
        }

        public async Task<Result> AtivarContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager.Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);

            var identityResult = await _userManager.ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao);

            if (identityResult.Succeeded)
                return Result.Ok();

            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}
