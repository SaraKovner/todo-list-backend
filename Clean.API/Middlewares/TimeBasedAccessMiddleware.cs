namespace Clean.API.Middlewares
{
    /// <summary>
    /// Middleware that blocks access to the site during specific hours
    /// Configured via appsettings.json AccessControl section
    /// </summary>
    public class TimeBasedAccessMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TimeBasedAccessMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get current hour (0-23)
            var currentHour = DateTime.Now.Hour;
            
            // Read blocked hours from configuration
            var blockedFromHour = _configuration.GetValue<int>("AccessControl:BlockedFromHour");
            var blockedToHour = _configuration.GetValue<int>("AccessControl:BlockedToHour");

            // Check if current time is within blocked hours
            if (currentHour >= blockedFromHour && currentHour < blockedToHour)
            {
                // Return 403 Forbidden status
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access to the site is blocked between 02:00-05:00");
                return; // Stop processing the request
            }

            // Continue to next middleware if not blocked
            await _next(context);
        }
    }
}