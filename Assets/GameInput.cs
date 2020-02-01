namespace DefaultNamespace
{
    public static class GameInput
    {
        public static string GetInput(int playerId, string buttonKey)
        {
            return buttonKey + playerId;
        }
    }
}