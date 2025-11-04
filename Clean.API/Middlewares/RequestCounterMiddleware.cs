namespace Clean.API.Middlewares
{
    /// <summary>
    /// Middleware that counts user requests using cookies
    /// Stores visit count in browser cookie and increments on each request
    /// </summary>
    public class RequestCounterMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Try to read existing request count from cookie
            var requestCountCookie = context.Request.Cookies["RequestCount"];
            int count = 1; // Default to 1 for first visit

            // If cookie exists and contains valid number, increment it
            if (requestCountCookie != null && int.TryParse(requestCountCookie, out int existingCount))
            {
                count = existingCount + 1;
            }

            // Update cookie with new count
            context.Response.Cookies.Append("RequestCount", count.ToString());
            
            // Continue to next middleware
            await _next(context);
            
            // Optional: Display visit count (currently commented out)
            //await context.Response.WriteAsync($"You have visited the site {count} times");
        }
    }
}
