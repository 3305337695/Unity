public interface ISaveable
{
    DataDefination GetGuid();

    void RegisterSaveData() => DataManager.instance.RegisterSaveData(this);

    void UnRegisterSaveData() => DataManager.instance.UnRegisterSaveData(this);

    void GetSaveData(Data data);

    void LoadSaveData(Data data);
}
