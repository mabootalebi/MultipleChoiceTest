using Domains.Entities;
using Domains.RepositoryInterfaces;
using Infrastructure.Db.Repositories;
using Microsoft.AspNetCore.Identity;
using Services;
using Services.Interfaces;

namespace API.Extensions
{
    public static class IoCExtension
    {
        public static void ResolveIoC(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<UserManager<User>>();
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<ITestCreatorServices, TestCreatorServices>();
            builder.Services.AddScoped<IAnswerTestServices, AnswerTestServices>();
            
            
            builder.Services.AddScoped<ITestRepository, TestRepository>();
            builder.Services.AddScoped<IChoiceRepository, ChoiceRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IAnalysisRepository, AnalysisRepository>();

        }

    }
}
