using System;

namespace OrderImport.Application.Core.Commands
{
    public class CommandBase : ICommand
    {
        public Guid Id { get; private set; }

        //public CommandBase()
        //{
        //    Id = Guid.NewGuid();
        //}

        //protected CommandBase(Guid id)
        //{
        //    Id = id;
        //}

        public void SetId(Guid id)
        {
            this.Id = id;
        }
    }

    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        public Guid Id { get; private set; }

        public void SetId(Guid id)
        {
            this.Id = id;
        }

        //protected CommandBase()
        //{
        //    Id = Guid.NewGuid();
        //}

        //protected CommandBase(Guid id)
        //{
        //    Id = id;
        //}
    }
}