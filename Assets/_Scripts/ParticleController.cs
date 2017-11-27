using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    public static ParticleController Instance;

    public ParticleDictionary[] ParticleContainer;
    public Dictionary<string, GameObject> Particles;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        InitializeParticles();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeParticles()
    {
        Particles = new Dictionary<string, GameObject>();
        foreach (var particle in ParticleContainer)
        {
            if (particle.Name != "" && particle.Particle != null && !Particles.ContainsKey(particle.Name))
                Particles.Add(particle.Name, particle.Particle);
        }
    }
    public void SpawnParticles(string particleTag, Vector2 position)
    {
        if(Particles.ContainsKey(particleTag))
        {
            GameObject particle = Instantiate(Particles[particleTag], position, Quaternion.identity) as GameObject;
            particle.transform.SetParent(transform);

            Destroy(particle, 5);
            //ParticleSystem ps = particle.GetComponent<ParticleSystem>();

        }
    }

    [System.Serializable]
    public class ParticleDictionary
    {
        public string Name = "sound";
        public GameObject Particle;
    }
}
