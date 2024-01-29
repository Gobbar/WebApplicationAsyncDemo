using Microsoft.EntityFrameworkCore;

namespace WebApplicationAsyncDemo.Models
{
    /// <summary>
    /// Хелпер работы с моделью PostgresDemo.
    /// </summary>
    public class PostgresDemoHelper
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private PostgresDemoDbContext _context;

        /// <summary>
        /// Создает хелпер.
        /// </summary>
        /// <param name="postgresDemosContext">Контекст базы данных.</param>
        public PostgresDemoHelper(PostgresDemoDbContext postgresDemosContext)
        {
            _context = postgresDemosContext;
        }

        /// <summary>
        /// Получает все записи PostgresDemos.
        /// </summary>
        /// <returns>Список PostgresDemos.</returns>
        public async Task<IEnumerable<PostgresDemo>> GetAllPostgresDemos()
        {
            return await _context.PostgresDemos.ToListAsync();
        }

        /// <summary>
        /// Получает запись PostgresDemos по Id.
        /// </summary>
        /// <param name="id">Id записи.</param>
        /// <returns>Запись PostgresDemos.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<PostgresDemo?> GetPostgresDemosById(int id)
        {
            return await _context.PostgresDemos.FindAsync(id);
        }

        /// <summary>
        /// Добавляет запись PostgresDemos.
        /// </summary>
        /// <param name="postgresDemo"></param>
        /// <returns></returns>
        public async Task<bool> InsertPostgresDemo(PostgresDemo postgresDemo)
        {
            var demoExists = await _context.PostgresDemos
                .AnyAsync(e => e.Id == postgresDemo.Id);
            
            if (demoExists)
            {
                return false;
            }

            _context.Add(postgresDemo);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Редактирует запись PostgresDemos по Id.
        /// </summary>
        /// <param name="postgresDemo">PostgresDemo содержащая Id и новые значения полей.</param>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<bool> UpdatePostgresDemo(PostgresDemo postgresDemo)
        {
            var demoExists = await _context.PostgresDemos
                .AnyAsync(e => e.Id == postgresDemo.Id);

            if (!demoExists)
            {
                return false;
            }

            _context.PostgresDemos.Attach(postgresDemo);
            _context.Entry(postgresDemo).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Удаляет запись PostgresDemos по Id.
        /// </summary>
        /// <param name="id">Id удаляемой записи.</param>
        /// <returns>Количество удаленных записей.</returns>
        public async Task<bool> DeletePostgresDemo(int id)
        {
            var demo = await _context.PostgresDemos.FindAsync(id);

            if (demo == null)
            {
                return false;
            }

            _context.PostgresDemos.Remove(demo);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
