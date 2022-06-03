public class MoveToActionData : ActionData {
    public int x;
    public int y;
    public int z;

    public MoveToActionData(int x, int y, int z)
    {
        this.type = "MoveTo";
        this.x = x;
        this.y = y;
        this.z = z;
    }
}