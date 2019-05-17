namespace csharp_sfml
{
    class CreateEnemy : ICreator
    {
        public CreateEnemy(){}

        /// <summary>
        ///   Create a new blank player
        /// </summary>
        /// <returns>IGameObj</returns>
        public IGameObj CreateObj()
        {
            return new Enemy();
        }
    }
}