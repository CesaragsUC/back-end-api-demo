namespace back_end
{
    /// <summary>
    /// Leia mais sobre Middlewares aqui: https://www.treinaweb.com.br/blog/compreendendo-os-middlewares-no-asp-net-core/
    /// </summary>
    public class MeuMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MeuMiddleware> _logger;
        public MeuMiddleware(RequestDelegate next, ILogger<MeuMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Código a ser executado antes do próximo middleware
            _logger.LogWarning("Mensagem do Middleware 1: Antes\n", DateTime.Now);

            // Chamada para o próximo middleware na cadeia
            await _next(context);

            // Código a ser executado depois do próximo middleware
            _logger.LogWarning("Mensagem do Middleware 1: Depois\n", DateTime.Now);

        }

    }

    public class MeuMiddleware2
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MeuMiddleware2> _logger;
        public MeuMiddleware2(RequestDelegate next, ILogger<MeuMiddleware2> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Código a ser executado antes do próximo middleware
            _logger.LogWarning("Mensagem do Middleware 2: Antes\n", DateTime.Now);

            // Chamada para o próximo middleware na cadeia
            await _next(context);

            // Código a ser executado depois do próximo middleware
            _logger.LogWarning("Mensagem do Middleware 2: Depois\n", DateTime.Now);

        }

    }

    public class MeuMiddleware3
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MeuMiddleware3> _logger;
        public MeuMiddleware3(RequestDelegate next, ILogger<MeuMiddleware3> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Código a ser executado antes do próximo middleware
            _logger.LogWarning("Mensagem do Middleware 3: Antes\n", DateTime.Now);

            // Chamada para o próximo middleware na cadeia
            await _next(context);

            // Código a ser executado depois do próximo middleware
            _logger.LogWarning("Mensagem do Middleware 3: Depois\n", DateTime.Now);

        }

    }
}
