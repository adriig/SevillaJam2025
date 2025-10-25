using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [Header("----------AUDIO SOURCE---------------")]

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource backgroundSource;
    [Header("----------AUDIO CLIP---------------")]

    public AudioClip background;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    public AudioClip backgroundMusicNaranjita;
    public AudioClip speakingNaranjita;
    public AudioClip leafMoving;
    public AudioClip backgroundMusicSkeleton;
    public AudioClip speakingSkeleton;
    public AudioClip backgroundMusicGolem;
    public AudioClip speakingGolem;
    public AudioClip backgroundMusicGhost;
    public AudioClip speakingGhost;
    public AudioClip backgroundMusicPixelart;
    public AudioClip speakingPixelart;
    public AudioClip backgroundMusicSlime;
    public AudioClip speakingSlime;
    public AudioClip backgroundMusicPyramid;
    public AudioClip speakingPyramid;
    public AudioClip cleaning;
    public AudioClip pickaxe;
    public AudioClip chisel;
    public AudioClip shiner;
    public AudioClip wateringCan;
    public AudioClip coloring;
    public AudioClip seeds;
    public AudioClip scissors;
    public AudioClip gloves;
    public AudioClip glovesHard;
    public AudioClip grimory;

    private void Awake()
    {
        // Si ya existe una instancia y no somos nosotros, nos destruimos
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asignamos la instancia y la mantenemos al cambiar de escena
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // --- MÉTODOS ESPECÍFICOS DE SONIDOS ---
    // Ambiente general
    public void PlayBackground() => PlayMusic(background);

    // Puertas
    public void PlayOpenDoor() => PlaySFX(openDoor);
    public void PlayCloseDoor() => PlaySFX(closeDoor);

    // Naranjita
    public void PlayBackgroundNaranjita() => PlayMusic(backgroundMusicNaranjita);
    public void PlaySpeakingNaranjita() => PlaySFX(speakingNaranjita);

    // Hojas
    public void PlayLeafMoving() => PlaySFX(leafMoving);

    // Esqueleto
    public void PlayBackgroundSkeleton() => PlayMusic(backgroundMusicSkeleton);
    public void PlaySpeakingSkeleton() => PlaySFX(speakingSkeleton);

    // Golem
    public void PlayBackgroundGolem() => PlayMusic(backgroundMusicGolem);
    public void PlaySpeakingGolem() => PlaySFX(speakingGolem);

    // Fantasma
    public void PlayBackgroundGhost() => PlayMusic(backgroundMusicGhost);
    public void PlaySpeakingGhost() => PlaySFX(speakingGhost);

    // Pixelart
    public void PlayBackgroundPixelart() => PlayMusic(backgroundMusicPixelart);
    public void PlaySpeakingPixelart() => PlaySFX(speakingPixelart);

    // Slime
    public void PlayBackgroundSlime() => PlayMusic(backgroundMusicSlime);
    public void PlaySpeakingSlime() => PlaySFX(speakingSlime);

    // Pirámide
    public void PlayBackgroundPyramid() => PlayMusic(backgroundMusicPyramid);
    public void PlaySpeakingPyramid() => PlaySFX(speakingPyramid);

    // Herramientas / acciones
    public void PlayCleaning() => PlaySFX(cleaning);
    public void PlayPickaxe() => PlaySFX(pickaxe);
    public void PlayChisel() => PlaySFX(chisel);
    public void PlayShiner() => PlaySFX(shiner);
    public void PlayWateringCan() => PlaySFX(wateringCan);
    public void PlayColoring() => PlaySFX(coloring);
    public void PlaySeeds() => PlaySFX(seeds);
    public void PlayScissors() => PlaySFX(scissors);
    public void PlayGloves() => PlaySFX(gloves);
    public void PlayGlovesHard() => PlaySFX(glovesHard);
    public void PlayGrimory() => PlaySFX(grimory);
}

