﻿using NServiceBus;
using __NAME__.App.Domain;
using __NAME__.Messages.Examples;

namespace __NAME__.App.Bus
{
    public class CloseExampleHandler : IHandleMessages<CloseExampleCommand>
    {
        private readonly IRepository _repository;

        public CloseExampleHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CloseExampleCommand message)
        {
            var entity = _repository.Load<ExampleEntity>(message.Id);
            entity.Close();
            _repository.Save(entity);
        }
    }
}
