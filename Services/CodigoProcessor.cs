using StackExchange.Redis;

namespace TarefaProcessorApi.Services
{
    public class CodigoProcessor
    {
        private readonly IDatabase _redisDb;

        public CodigoProcessor(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task<bool> ProcessarCodigoAsync(string codigo)
        {
            var key = $"codigo:{codigo}";
            var token = Guid.NewGuid().ToString();

            // Tenta setar a chave se ela ainda não existir (SET NX)
            var acquired = await _redisDb.StringSetAsync(
                key,
                token,
                TimeSpan.FromMinutes(5), // TTL de 5 minutos
                when: When.NotExists
            );

            if (!acquired)
            {
                // Já está sendo processado ou já foi
                return false;
            }

            try
            {
                // Simula o processamento do código
                Console.WriteLine($"Processando código: {codigo}");

                // Aguarda 1s para simular processamento
                await Task.Delay(1000);

                return true;
            }
            finally
            {
                // Remove a chave se ainda for o dono (evita conflitos em concorrência)
                var currentValue = await _redisDb.StringGetAsync(key);
                if (currentValue == token)
                {
                    await _redisDb.KeyDeleteAsync(key);
                }
            }
        }
    }
}
