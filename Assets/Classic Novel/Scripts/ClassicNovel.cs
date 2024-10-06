using System.Collections;
using System.Collections.Generic;
using Dunward.Capricorn;
using UnityEngine;
using UnityEngine.UI;

public class ClassicNovel : MonoBehaviour
{
    [SerializeField]
    private CapricornRunner runner;

    [SerializeField]
    private Button interactionPanel;

    [SerializeField]
    private TextAsset asset;
    
    [SerializeField]
    private Transform selectionArea;

    [SerializeField]
    private GameObject selectionPrefab;

    private void Start()
    {
        runner.Load(asset.text);
        runner.bindingInteraction = interactionPanel.onClick;

        runner.onSelectionCreate += (selections) =>
        {
            var buttons = new List<Button>();

            foreach (var selection in selections)
            {
                var button = Instantiate(selectionPrefab, selectionArea).GetComponent<Button>();
                button.GetComponentInChildren<Text>().text = selection;
                buttons.Add(button);

                button.onClick.AddListener(() =>
                {
                    foreach (var button in buttons)
                    {
                        Destroy(button.gameObject, .5f);
                    }
                });
            }

            return buttons;
        };

        StartCoroutine(runner.Run());
    }
}
