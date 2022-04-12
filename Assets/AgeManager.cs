using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AgeManager : MonoBehaviour
{
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI ageNotificationText;

    public int currentAge;
    public int ageModifier;
    public int agingRate;
    public int startingAge;
    public float notificationLength;
    [SerializeField]private float notificationTimer;

    public static Action<int> Age;
    public static Action<int> StartingAge;

    private void OnEnable()
    {
        PlayerController.currentTile += CheckAgeModification;
        AgeModifier.modifiedAge += GetAgeModification;
        Swapper.resetAgeRequest += ResetAge;
    }


    private void OnDisable()
    {
        PlayerController.currentTile -= CheckAgeModification;
        AgeModifier.modifiedAge -= GetAgeModification;
        Swapper.resetAgeRequest -= ResetAge;
    }

    public void GetAgeModification(int ageModification, string tile)
    {
        if (tile == "Ager")
        {
            AddAge(ageModification);
        }
        else if (tile == "Reducer")
        {
            MinusAge(ageModification);
        }
        else if (tile == "Reset")
        {
            ResetAge();
            SetAgeNotification($"{ageModification}");
        }

        SetAgeNotification($"+{ageModification}");
    }

    public void CheckAgeModification(Collider2D tile)
    {
        if (tile.tag != "Ager" && tile.tag != "Reducer" && tile.tag != "Reset")
            AddAge(agingRate);
    }

    public void AddAge(int age)
    {
        currentAge += age;
        SetAgeNotification($"+{age}");
        Broadcaster.Send(Age, currentAge);
    }

    public void MinusAge(int age)
    {
        currentAge -= age;
        SetAgeNotification($"+{age}");
        Broadcaster.Send(Age, currentAge);
    }

    public void ResetAge()
    {
        currentAge = startingAge;
        Broadcaster.Send(Age, currentAge);
    }

    public void SetAgeNotification(string notif)
    {
        if (!ageNotificationText.gameObject.activeSelf)
        {
            ageNotificationText.gameObject.SetActive(true);
            ageNotificationText.text = notif;
            notificationTimer = notificationLength;
        }
    }

    public void NotificationTimer()
    {
        if (notificationTimer > 0)
        {
            notificationTimer -= Time.deltaTime;
            if (notificationTimer <= 0)
            {
                notificationTimer = 0;
                ageNotificationText.gameObject.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetAge();
        Broadcaster.Send(StartingAge, startingAge);
    }

    // Update is called once per frame
    void Update()
    {
        ageText.text = $"Age: {currentAge.ToString()}";
        NotificationTimer();

    }
}
