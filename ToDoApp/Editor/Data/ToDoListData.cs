namespace ToDoApp.Editor.Data;

public class ToDoListData {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<ToDoListItemData> Items { get; set; } = Enumerable.Empty<ToDoListItemData>();

}
