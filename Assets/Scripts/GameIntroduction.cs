using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameIntroduction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] text;
    [SerializeField]
    private Animator anim;

    public void StartGameIntroduction() 
    {
        StartCoroutine(PlayGameIntroduction());
    }

    private void PlaySound(int i) 
    {

        switch (i)
        {
            case 0:
                SoundManager.Instance.SFXSounds(SoundTypes.Fighthng);
                Debug.Log("Playing intro sound" + SoundTypes.Fighthng);
                break;
            case 1:
                SoundManager.Instance.SFXSounds(SoundTypes.LaserShooting);
                Debug.Log("Playing intro sound" + SoundTypes.LaserShooting);
                break;
            case 2:
                SoundManager.Instance.SFXSounds(SoundTypes.ShipFallingDown);
                Debug.Log("Playing intro sound" + SoundTypes.ShipFallingDown);
                break;
            case 3:
                SoundManager.Instance.SFXSounds(SoundTypes.TheUnknownDark);
                Debug.Log("Playing intro sound" + SoundTypes.TheUnknownDark);
                break;
        }

    }
    private IEnumerator PlayGameIntroduction()
    {
        SoundManager.Instance.StopPlayingBGSound();
        for (int i = 0; i < text.Length; i++)
        {
            text[i].gameObject.SetActive(true);
            Debug.Log("Showing text " + text[i].text);

           // anim.SetInteger("ShowText", i);
            Debug.Log("Intro anim triggered");

            // Wait for 2.5 seconds before showing the next text
            float soundwaittime = anim.runtimeAnimatorController.animationClips[i].length / 1.3f; // sounds plays after 80% - 75% of anim is played 
            yield return new WaitForSeconds(soundwaittime);
            PlaySound(i);         

            // Assuming you want to wait for the animation to complete before moving to the next text
            float waitTime = anim.runtimeAnimatorController.animationClips[i].length - soundwaittime;
            Debug.Log("Anim clip time is " + waitTime);
            yield return new WaitForSeconds(waitTime);

            Debug.Log("Moving to next text");
        }

        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
        SoundManager.Instance.startPlayingBGSound();
    }

}

