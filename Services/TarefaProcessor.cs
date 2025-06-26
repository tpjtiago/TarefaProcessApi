using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using TarefaProcessorApi.Data;
using TarefaProcessorApi.Models;

namespace TarefaProcessorApi.Services
{
    public class TarefaProcessor
    {
        private readonly IDatabase _redisDb;
        private readonly MyDbContext _dbContext;

        public TarefaProcessor(IConnectionMultiplexer redis, MyDbContext dbContext)
        {
            _redisDb = redis.GetDatabase();
            _dbContext = dbContext;
        }

        public async Task ProcessarTarefasAsync()
        {
            var tarefas = await _dbContext.Tarefas
                .Where(t => t.Status == "PENDING")
                .Take(10)
                .ToListAsync();

            foreach (var tarefa in tarefas)
            {
                var lockKey = $"lock:tarefa:{tarefa.Id}";
                var lockToken = Guid.NewGuid().ToString();
                var lockAcquired = await _redisDb.StringSetAsync(
                    lockKey,
                    lockToken,
                    TimeSpan.FromSeconds(30),
                    when: When.NotExists
                );

                if (!lockAcquired)
                    continue;

                try
                {
                    tarefa.Status = "PROCESSING";
                    tarefa.DataInicioProcessamento = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();

                    await ProcessarAsync(tarefa);

                    tarefa.Status = "DONE";
                    tarefa.DataFimProcessamento = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                }
                finally
                {
                    var currentValue = await _redisDb.StringGetAsync(lockKey);
                    if (currentValue == lockToken)
                    {
                        await _redisDb.KeyDeleteAsync(lockKey);
                    }
                }
            }
        }

        private Task ProcessarAsync(Tarefa tarefa)
        {
            Console.WriteLine($"Processando tarefa {tarefa.Id}...");
            return Task.Delay(1000); // simula trabalho
        }
    }
}
