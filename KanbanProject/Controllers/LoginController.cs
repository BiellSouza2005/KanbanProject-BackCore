﻿using KanbanProject.Data;
using KanbanProject.Data.DTOs.Login;
using KanbanProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
            private readonly AppDbContext _context;

            public LoginController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost("LoginUsuario")]
            public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
            {
                // Verifica se existe um usuário com o email fornecido
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                // Se o usuário não for encontrado ou a senha estiver incorreta
                if (user == null || user.Password != loginDto.Password)
                {
                    return BadRequest("Usuário ou senha incorretos.");
                }

                // Se estiver tudo certo, retornar o token de acesso e uma mensagem de sucesso
                return Ok(new { mensagem = "Login realizado com sucesso!", token = Guid.NewGuid(), user });
            }
    }
}
