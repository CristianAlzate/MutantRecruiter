using Azure.Storage.Queues;
using MutantRecruiter.Services.Contract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutantRecruiter.Services.Services
{
    public class QueueService<T> : IQueueService<T> where T : class
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        public QueueService(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
        }
        public async void QueueStack(T messageObject)
        {
            QueueClient queueClient = new QueueClient(_connectionString, _queueName,new QueueClientOptions() { MessageEncoding = QueueMessageEncoding.Base64});
            string message = JsonConvert.SerializeObject(messageObject);
            queueClient.SendMessage(message);
        }
    }
}
