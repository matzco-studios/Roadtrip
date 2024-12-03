using System.Collections;
using System.Collections.Generic;
using Items.Mechanics;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


	public class Door : Interactable
	{
		FadeInOut fade;
		public bool open;
		public bool CanOpen;
		public float smooth = 1.0f;
		float DoorOpenAngle = -90.0f;
		float DoorCloseAngle = 0.0f;
		public AudioSource asource;
		public AudioClip openDoor, closeDoor;
		[SerializeField] private UI.ActionMessageController _message;
		// Use this for initialization
		void Start()
		{
			fade = FindObjectOfType<FadeInOut>();
			asource = GetComponent<AudioSource>();
			_message = GetMessage();
		}

		// Update is called once per frame
		void Update()
		{
			if (open)
			{
				var target = Quaternion.Euler(0, DoorOpenAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);

			}
			else
			{
				var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);

			}
		}

		public IEnumerator ChangeScene()
		{
			fade.FadeIn();
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene(2);
		}

		public void OpenDoor()
		{
			open = !open;
			asource.clip = open ? openDoor : closeDoor;
			asource.Play();
			StartCoroutine(ChangeScene());
		}

        public override void OnInteract()
        {
            OpenDoor();
        }

        public override void InteractionMessage()
        {
            _message.InteractableItem("Door", "to open");
        }
    }
}