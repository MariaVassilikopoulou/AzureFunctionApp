//using System;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Azure.Functions.Worker.Http;
//using Microsoft.Azure.Functions.Worker.Extensions.Sql;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;

//namespace TheFunctionAppAzure
//{
//    public class Function1
//    {
//        private readonly ILogger _logger;

//        public Function1(ILoggerFactory loggerFactory)
//        {
//            _logger = loggerFactory.CreateLogger<Function1>();
//        }

//        // Visit https://aka.ms/sqltrigger to learn how to use this trigger binding
//        [Function("Function1")]
//        public void Run(
//            [SqlTrigger("[dbo].[table1]", "ConnectionStrings:DefaultConnection")] IReadOnlyList<SqlChange<ToDoItem>> changes,
//                FunctionContext context)
//        {
//            _logger.LogInformation("SQL Changes: " + JsonConvert.SerializeObject(changes));

//        }
//    }

//    public class ToDoItem
//    {
//        public string Id { get; set; }
//        public int Priority { get; set; }
//        public string Description { get; set; }
//    }
//}
