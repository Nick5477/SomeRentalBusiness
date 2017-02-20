namespace Domain.Commands
{
    public interface ICommandBuilder
    {
        void Execute<TCommandContext>(TCommandContext commandContext)
            where TCommandContext : ICommandContext;
    }
}
