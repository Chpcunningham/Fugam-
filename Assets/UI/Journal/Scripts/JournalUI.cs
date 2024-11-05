using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalUI : MonoBehaviour
{
    [SerializeField] private JournalContainerUI _journalContainerUI;
    [SerializeField] private Animation _animation;
    [SerializeField] private PlayerInteractController _playerInteractController;

    private bool _shown = false;
    private float _toggleCooldown = 0;

    public bool IsShown { get { return _shown; } }

    private void Awake()
    {
        _playerInteractController = FindObjectOfType<PlayerInteractController>();
    }

    void Update()
    {
        _toggleCooldown -= Time.deltaTime;
        if (_toggleCooldown < 0 ) { _toggleCooldown = 0; }
    }

    public JournalContainerUI GetContainerUI()
    {
        return _journalContainerUI;
    }

    public void TryToggle()
    {
        if (_toggleCooldown > 0)
        {
            return;
        }
        _toggleCooldown = 1;

        string clipName;

        if (_shown)
        {
            clipName = "JournalClose";
            _shown = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _playerInteractController.SetUIPauseControls(false);
        } else
        {
            clipName = "JournalOpen";
            _shown = true;
            _playerInteractController.SetUIPauseControls(true);
        }
        _animation.Play(clipName);
        Invoke("_setCursor", _animation.GetClip(clipName).length / 2);
    }

    private void _setCursor()
    {
        
        if (_shown)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
