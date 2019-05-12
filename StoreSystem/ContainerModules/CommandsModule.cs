using Autofac;
using WIMSystem.Commands.AddCommands;
using WIMSystem.Commands.ChangeCommands;
using WIMSystem.Commands.Contracts;
using WIMSystem.Commands.CreateCommands;
using WIMSystem.Commands.ListCommands;
using WIMSystem.Commands.Utils;
using WIMSystem.Core.Factories;
using WIMSystem.Core.Factories.Contracts;

namespace WIMSystem.ContainerModules
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandsFactory>().As<ICommandsFactory>();
            builder.RegisterType<Getters>().As<IGetters>();

            //builder.RegisterType<AddBoardToTeamCommand>().Named<IEngineCommand>("AddBoardToTeam");
            builder.RegisterType<AddPersonToTeamCommand>().Named<IEngineCommand>("AddPersonToTeam");
            builder.RegisterType<CreatePersonCommand>().Named<IEngineCommand>("CreatePerson");
            builder.RegisterType<CreateTeamCommand>().Named<IEngineCommand>("CreateTeam");
            builder.RegisterType<CreateBoardCommand>().Named<IEngineCommand>("CreateBoard");
            builder.RegisterType<CreateBugCommand>().Named<IEngineCommand>("CreateBug");
            builder.RegisterType<CreateStoryCommand>().Named<IEngineCommand>("CreateStory");
            builder.RegisterType<CreateFeedbackCommand>().Named<IEngineCommand>("CreateFeedback");
            builder.RegisterType<CreateCommentCommand>().Named<IEngineCommand>("CreateComment");
            builder.RegisterType<ChangePriorityCommand>().Named<IEngineCommand>("ChangePriority");
            builder.RegisterType<ChangeRatingOfFeedbackCommand>().Named<IEngineCommand>("ChangeRatingOfFeedback");
            builder.RegisterType<ChangeSeverityOfBugCommand>().Named<IEngineCommand>("ChangeSeverityOfBug");
            builder.RegisterType<ChangeSizeOfStoryCommand>().Named<IEngineCommand>("ChangeSizeOfStory");
            builder.RegisterType<ChangeStatus>().Named<IEngineCommand>("ChangeStatus");
            builder.RegisterType<AssignWorkItemToMemberCommand>().Named<IEngineCommand>("AssignWorkItemToMember");
            builder.RegisterType<UnassignWorkItemCommand>().Named<IEngineCommand>("UnassignWorkItem");
            builder.RegisterType<ListBoardWorkItemsCommand>().Named<IEngineCommand>("ListBoardWorkItems");
            builder.RegisterType<ShowAllPeopleCommand>().Named<IEngineCommand>("ShowAllPeople");
            builder.RegisterType<ShowAllTeamBoardsCommand>().Named<IEngineCommand>("ShowAllTeamBoards");
            builder.RegisterType<ShowAllTeamMembersCommand>().Named<IEngineCommand>("ShowAllTeamMembers");
            builder.RegisterType<ShowAllTeamsCommand>().Named<IEngineCommand>("ShowAllTeams");
            builder.RegisterType<ShowBoardActivityCommand>().Named<IEngineCommand>("ShowBoardActivity");
            builder.RegisterType<ShowPersonActivityCommand>().Named<IEngineCommand>("ShowPersonActivity");
            builder.RegisterType<ShowTeamActivityCommand>().Named<IEngineCommand>("ShowTeamActivity");

        }
    }
}
