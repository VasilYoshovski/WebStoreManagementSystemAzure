using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Commands.Utils
{
    internal class CommandsConsts
    {
        internal const string TerminationCommand = "exit";
        internal const string TerminationAppCommand = "appexit";
        internal const string ConsoleExitCommand = "";

        internal const string InvalidCommand = "Invalid command name: {0}!";
        internal const string EmptyCommand = "Command name: {0} can not be null or empty!";

        internal const string ObjectExists = "{0} with name {1} already exists!";
        internal const string ObjectCreated = "{0} with name {1} was created!";
        internal const string ObjectDoesNotExist = "{0} {1} does not exist!";
        internal const string ObjectAddedToTeam = "{0} {1} added to team \"{2}\"!";
        internal const string ObjectRemovedFromTeam = "{0} {1} removed from team \"{2}\"!";
        internal const string WorkItemAssigned = "{0} work item is assigned to member {1}!";
        internal const string WorkItemUnAssigned = "{0} work item is unassigned from member {1}!";
        internal const string WorkItemStatusChange = "{0} work item's status is changed to {1}!";
        internal const string FeedbackRatingChange = "{0} feedback's rating is changed to {1}!";
        internal const string WorkItemPriorityChange = "{0} work item's priority is changed to {1}!";
        internal const string StorySizeChange = "{0} story's size is changed to {1}!";
        internal const string BugSeverityChange = "{0} bug's severity is changed to {1}!";
        internal const string CommentAdded = "Comment \"{0}\" with author {1} added to \"{2}\"!";
        internal const string NotMemberOfAnyTeam = "{0} is not a member of any team!";

        internal const string NoTeamFound = "No {0} team found!";
        internal const string NoBoardFound = "No {0} board found!";
        internal const string NoPersonFound = "No {0} person found!";
        internal const string NoWorkItemFound = "No {0} work item found!";
        internal const string NoPersonInTeamFound = "There is no person with name {0} in the team.";

        //Common
        internal const string INVALID_QUANTITY = "{0} {1} have to be more than {2}! It was passed {3}";
        internal const string INVALID_PROPERTY_LENGTH = "{0} {1} can not be less than {2} or more than {3} chars! It was passed {4}";
        internal const string NULL_PROPERTY_NAME = "{0} {1} can not be null!";
        internal const string NULL_OBJECT = "Passed {0} can not be null!";
        internal const string NOT_FOUND_OBJECT = "Passed {0} can not be found!";
        internal const string SPLIT_CHAR = ",";
    }
}
