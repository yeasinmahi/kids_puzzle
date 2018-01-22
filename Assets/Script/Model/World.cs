public class World {
    [PrimaryKey]
    public int Sl { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int TargetedToy { get; set; }
    public int IsReady { get; set; }
    public string UpdateDate { get; set; }
}
