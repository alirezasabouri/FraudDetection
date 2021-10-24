using Dapper.Contrib.Extensions;
using FraudDetection.Adapter.Database.Entities;
using FraudDetection.Contracts.Ports;
using FraudDetection.Misc;
using FraudDetection.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetection.Adapter.Database.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDBConnectionFactory _connectionFactory;
        private readonly ILogger _logger;

        public PersonRepository(IDBConnectionFactory connectionFactory, ILogger logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<Result> CreateAsync(Person person)
        {
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                {
                    await connection.InsertAsync(PersonEntity.FromModel(person));
                }
            }
            catch (Exception exp)
            {
                _logger.Error(exp, "Error inserting person entity {person}", person);
                return Result.Fail(exp);
            }

            return Result.Success();
        }

        public async Task<Result<IReadOnlyCollection<Person>>> GetAllAsync()
        {
            try
            {
                IEnumerable<PersonEntity> entities = null;
                using (var connection = _connectionFactory.CreateConnection())
                {
                    entities = await connection.GetAllAsync<PersonEntity>();
                }

                var result = entities.Select(pe => pe.ToModel()).ToList();
                return Result.Success<IReadOnlyCollection<Person>>(result);
            }
            catch (Exception exp)
            {
                _logger.Error(exp, "Error fetching person entities");
                return Result<IReadOnlyCollection<Person>>.Fail(exp);
            }
        }
    }
}
