using Shy;
using TMPro;
using UnityEngine;

public class SkillDescriptionPanel : MonoBehaviour
{
    private TMP_Text _desc;
    [SerializeField] private int _index;

    private void Awake()
    {
        _desc = GetComponentInChildren<TMP_Text>();
    }

    public void SetUpDate(CharacterSO data)
    {
        Debug.Log(_desc);
        Debug.Log(data);
        Debug.Log(data.skills);
        Debug.Log(data.skills[_index]);
        _desc.text = data.skills[_index].explian;
    }
}
