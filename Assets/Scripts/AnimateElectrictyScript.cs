using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateElectrictyScript : MonoBehaviour {
  public Sprite groundState1;
	public Sprite groundState2;

	public Sprite zap1;
	public Sprite zap2;
	public Sprite zap3;
    CircleCollider2D circleCollider;
	List<Sprite> growingZaps;
	int currentZapGrowth = 0;

	public Sprite bigzap1;
	public Sprite bigzap2;

	float timeBetweenZaps = 3;
	float timeLengthOfZaps = 1;

	float zapTime = 0;
	float animateTime = 0;

	float animationFrameLength = .2f;
    public float groundRadius = 1.5f;
    public float growingRadius = 2f;
    public float bigZapRadius = 3f;

    Sprite currentCostume;

	public enum electricityState
	{
		groundState,
		growingZap,
		bigZap
	}

	electricityState zapState;

	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		zapState = electricityState.groundState;
		spriteRenderer = GetComponent<SpriteRenderer>();
		currentCostume = groundState1;
		spriteRenderer.sprite = currentCostume;
		growingZaps = new List<Sprite>();
		growingZaps.Add(zap1); growingZaps.Add(zap2); growingZaps.Add(zap3);
        circleCollider = GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {
		zapTime += Time.deltaTime;
		animateTime += Time.deltaTime;

		switch (zapState)
		{
			case electricityState.groundState:
                circleCollider.radius = groundRadius;
				if (animateTime >= animationFrameLength)
				{
					toggleCostume(groundState1, groundState2);
					animateTime = 0;
				}
				if (zapTime >= timeBetweenZaps)
				{
					zapTime = 0;
					zapState = electricityState.growingZap;
					currentZapGrowth = 0;
					currentCostume = growingZaps[0];
				}
				break;
			case electricityState.growingZap:
                circleCollider.radius = growingRadius;

                if (animateTime >= animationFrameLength)
				{
					currentZapGrowth++;
					if (currentZapGrowth <= 2)
					{
						currentCostume = growingZaps[currentZapGrowth];
					} else {
						currentZapGrowth = 0;
						currentCostume = bigzap1;
						zapState = electricityState.bigZap;
						zapTime = 0;

					}
					animateTime = 0;
				}
				break;
			case electricityState.bigZap:
                circleCollider.radius = bigZapRadius;

                if (animateTime >= animationFrameLength)
			{
				toggleCostume(bigzap1, bigzap2);
				animateTime = 0;
			}
			if (zapTime >= timeLengthOfZaps)
			{
				zapTime = 0;
				zapState = electricityState.groundState;

			}
			break;
		}
		spriteRenderer.sprite = currentCostume;
	}

	//Takes two Sprites and switches to the first one.  If it's already the first
	//one then switch to the second.
	void toggleCostume(Sprite costume1, Sprite costume2)
	{
		if (currentCostume == costume1){
			currentCostume = costume2;
		} else {
			currentCostume = costume1;
		}
	}
}
