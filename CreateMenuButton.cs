namespace csharp_sfml
{
    public class CreateMenuButton : ICreator
    {
        public CreateMenuButton(){}

        /// <summary>
        /// Create a new blank menu button
        /// </summary>
        /// <returns>IGameObj</returns>
        public IGameObj CreateObj()
        {
            return new MenuButton();
        }
    }
}