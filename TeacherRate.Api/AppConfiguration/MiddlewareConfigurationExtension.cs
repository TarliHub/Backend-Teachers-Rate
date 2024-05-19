namespace TeacherRate.Api.AppConfiguration;

public static class MiddlewareConfigurationExtension
{
    public static IApplicationBuilder ConfigureMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.ApplyMigrations();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors();

        app.UseHttpsRedirection();

        //app.UseSession();

        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
