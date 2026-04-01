namespace Game.MyModule
{
    public sealed class MyModuleStatusModel
    {
        public string StageName { get; set; } = "MyModuleStage";

        public string ViewName { get; set; } = "UIPanelLogin";

        public int RecommendedPower { get; set; } = 1000;

        public bool Ready { get; set; }
    }
}
