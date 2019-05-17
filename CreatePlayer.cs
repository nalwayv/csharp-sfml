namespace csharp_sfml
{
    class CreatePlayer : ICreator
    {
        public CreatePlayer(){}

        /// <summary>
        ///   Create a new blank player
        /// </summary>
        /// <returns>IGameObj</returns>
        public IGameObj CreateObj()
        {
            return new Player();
        }
    }
}
