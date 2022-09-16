namespace ToDoApp.Permissions;

public static class ToDoAppPermissions
{
    public const string GroupName = "ToDoApp";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public const string ToDoCreation = GroupName + ".ToDoCreation";
    public const string ToDoDeletion = GroupName + ".ToDoDeletion";
    public const string GetToDo = GroupName + ".GetToDo";
    public const string UpdateToDo = GroupName + ".UpdateToDo";
}
