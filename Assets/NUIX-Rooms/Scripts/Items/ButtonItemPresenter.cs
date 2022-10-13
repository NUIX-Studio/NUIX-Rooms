public class ButtonItemPresenter : ItemPresenter
{
    public void Press()
    {
        actions.Invoke();
    }

    public ButtonItemPresenter()
    {
        actions.AddListener(Press);
    }
}