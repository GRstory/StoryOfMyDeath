using I2.Loc;
using System.Text;

public static class DialogDBManager
{
    private static StringBuilder _sb = new StringBuilder();

    public static string GetText(int npcId, int dialogOrder)
    {
        MakeDialogKey(npcId, dialogOrder);

        return LocalizationManager.GetTranslation(_sb.ToString());
    }

    private static void MakeDialogKey(int npcId, int dialogOrder)
    {
        _sb.Clear();
        _sb.Append("Dialog/" + npcId.ToString() + "_" + dialogOrder.ToString());
    }
}