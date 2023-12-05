using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// </summary>
public class ResourceSystem : StaticInstance<ResourceSystem> {
    public List<ScriptableUnitBase> ExampleHeroes { get; private set; }
    private Dictionary<string, ScriptableUnitBase> _ExampleHeroesDict;

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        ExampleHeroes = Resources.LoadAll<ScriptableUnitBase>("ExampleHeroes").ToList();
        _ExampleHeroesDict = ExampleHeroes.ToDictionary(r => r.Name, r => r);
    }

    public ScriptableUnitBase GetExampleHero(string t) => _ExampleHeroesDict[t];
    public ScriptableUnitBase GetRandomHero() => ExampleHeroes[Random.Range(0, ExampleHeroes.Count)];
}   