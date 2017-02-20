using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Services;

namespace Infrastructure.Commands
{
    public class ReturnBikeCommand:ICommand<ReturnBikeCommandContext>
    {
        private readonly IRentService _rentService;

        public ReturnBikeCommand(IRentService rentService)
        {
            _rentService = rentService;
        }
        public void Execute(ReturnBikeCommandContext commandContext)
        {
            _rentService.Return(commandContext.Bike,commandContext.RentPoint,commandContext.IsBroken);
        }
    }
}
