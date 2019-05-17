namespace csharp_sfml
{
    public class CreateAnimatedText : ICreator
    {
        public CreateAnimatedText(){}

        /// <summary>
        /// Create a new blank menu button
        /// </summary>
        /// <returns>IGameObj</returns>
        public IGameObj CreateObj()
        {
            return new AnimatedTexture();
        }
    }
}