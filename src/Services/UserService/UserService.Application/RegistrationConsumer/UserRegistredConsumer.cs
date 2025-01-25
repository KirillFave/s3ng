using AutoMapper;
using Confluent.Kafka;
using MediatR;
using SharedLibrary.Common.Kafka.Messages;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Results;
using UserService.KafkaConsumer.Options;
using ILogger = Serilog.ILogger;

namespace UserService.KafkaConsumer.Consumers
{
    public class UserRegistredConsumer : ConsumerBackgroundService<string, UserRegistredMessage>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserRegistredConsumer(IMediator mediator, IMapper mapper, ILogger logger, ApplicationOptions applicationOptions)
            : base(logger, applicationOptions)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        protected override string TopicName { get; } = "registration_events";

        protected override async Task HandleAsync(ConsumeResult<string, UserRegistredMessage> message, CancellationToken cancellationToken)
        {
            if (message?.Message?.Value == null)
            {
                _logger.Error("UserRegistredMessage is null");
            }

            var createUserReq = _mapper.Map<CreateUserRequestDto>(message.Message.Value);
            var createUserRsp = await _mediator.Send(createUserReq, cancellationToken);

            if (createUserRsp.Result != CreateUserResultModel.Success)
            {
                _logger.Error($"Create user result: {createUserRsp.Result}");
            }

            _logger.Error($"Create user result is success, UserId = {createUserRsp.User.Id}");
        }
    }
}
