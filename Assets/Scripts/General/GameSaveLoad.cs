using System.IO;
using Newtonsoft.Json;

namespace General
{
    public static class GameSaveLoad
    {
        private const string SAVE_PATH = "save.dat";


        public static void Save(GameStateData gameStateData)
        {
            if(File.Exists(SAVE_PATH))
                File.Delete(SAVE_PATH);
            string json = JsonConvert.SerializeObject(gameStateData);
            File.WriteAllText(SAVE_PATH, json);
        }

        public static GameStateData Load()
        {
            if (!File.Exists(SAVE_PATH))
                return null;
            
            GameStateData gameStateData = new GameStateData();
            string json = File.ReadAllText(SAVE_PATH);
            gameStateData = JsonConvert.DeserializeObject<GameStateData>(json);
            File.Delete(SAVE_PATH);
            return gameStateData;
        }

        public static bool IsPosibleToLoadLevel()
        {
            return File.Exists(SAVE_PATH);
        }
    }
}
