namespace WIMSystem.Menu.Contracts
{
    interface IMenuItem
    {
        string CommandText { get; set; }
        string MenuText { get; set; }
        string ParamsText { get; set; }
    }
}