using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Tools;

public class End : MonoSingleton<End>
{
    public bool isFinish
    {
        get { return _isFinish; }
    }

    [Header("Infos")]
    [SerializeField] Validator _validator;
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _cameraEnd;
    [SerializeField] Text _endText;
    [SerializeField] float _timePerLetter = 1;


    [Header("Container Mapping")]
    [SerializeField] Container _pelvisContainer;
    [SerializeField] Container _torsoContainer;
    [SerializeField] Container _headContainer;
    [SerializeField] Container _leftArm0Container;
    [SerializeField] Container _leftArm1Container;
    [SerializeField] Container _rightArm0Container;
    [SerializeField] Container _rightArm1Container;
    [SerializeField] Container _leftLeg0Container;
    [SerializeField] Container _leftLeg1Container;
    [SerializeField] Container _rightLeg0Container;
    [SerializeField] Container _rightLeg1Container;

    [Header("Anim End Mapping")]
    [SerializeField] Transform _pelvis;
    [SerializeField] Transform _torso;
    [SerializeField] Transform _head;
    [SerializeField] Transform _leftArm0;
    [SerializeField] Transform _leftArm1;
    [SerializeField] Transform _rightArm0;
    [SerializeField] Transform _rightArm1;
    [SerializeField] Transform _leftLeg0;
    [SerializeField] Transform _leftLeg1;
    [SerializeField] Transform _rightLeg0;
    [SerializeField] Transform _rightLeg1;

    [Header("Anims")]
    [SerializeField] List<Anim> _anims = new List<Anim>();

    bool _isFinish = false;
    string _string;
    int _letterIndex;
    float _currentLetterTime;

    bool _isWritting;

    void Awake()
    {
        for (int i = 0; i < _anims.Count; i++)
        {
            _anims[i].Init();
        }
    }

    public void Actualize()
    {
        _validator.SetAnim(ContainerManager.instance.CanFinish());

        if(Input.GetMouseButtonDown(0))
        {
            Vector2 wpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Rect r = _validator.GetCollider();

            if (r.Contains(wpos))
            {
                AskForEnd();
            }
        }

        if(_isWritting)
        {
            _currentLetterTime += Time.deltaTime;

            if(_currentLetterTime > _timePerLetter)
            {
                _currentLetterTime -= _timePerLetter;
                _letterIndex++;

                if(_letterIndex == _string.Length)
                {
                    _isWritting = false;
                }
            }

            _endText.text = _string.Substring(0, _letterIndex);
        }
    }

    void AskForEnd()
    {
        if (ContainerManager.instance.CanFinish())
        {
            _canvas.SetActive(true);
            _cameraEnd.SetActive(true);

            _string = ContainerManager.instance.GetMessage();
            _isWritting = true;
            _isFinish = true;

            Map();

            for (int i = 0; i < _anims.Count; i++)
            {
                _anims[i].Init();
            }
        }
    }

    void Map()
    {
        Copy(_pelvisContainer, _pelvis);
        Copy(_torsoContainer, _torso);
        Copy(_headContainer, _head);
        Copy(_leftArm0Container, _leftArm0);
        Copy(_leftArm1Container, _leftArm1);
        Copy(_rightArm0Container, _rightArm0);
        Copy(_rightArm1Container, _rightArm1);
        Copy(_leftLeg0Container, _leftLeg0);
        Copy(_leftLeg1Container, _leftLeg1);
        Copy(_rightLeg0Container, _rightLeg0);
        Copy(_rightLeg1Container, _rightLeg1);
    }

    void Copy(Container container, Transform parent)
    {
        GameObject go = Instantiate(container.containing.gameObject, parent);
        go.layer = 8;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }

    public void OnRetry()
    {
        SceneManager.instance.Fade();
    }
}
