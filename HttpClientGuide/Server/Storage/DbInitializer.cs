namespace HttpClientGuide.Server.Storage
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            await context!.Database.EnsureCreatedAsync();
            // If there are no users in the database
            if (context.Users.Count() < 20)
            {
                await context.Users.AddRangeAsync(UserGenerator.GenerateUsers(20 - context.Users.Count()));
            }
            await context.SaveChangesAsync();
        }
    }
}
