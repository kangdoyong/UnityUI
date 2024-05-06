using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PageSwiper : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [Header("Int")]
    public int startingPage = 0;
    private int _pageCount;
    private int _currentPage;
    private int _fastSwipeThresholdMaxLimit;
    public int fastSwipeThresholdDistance = 100;
    private int _previousPageSelectionIndex;

    [Header("Float")]
    public float fastSwipeThresholdTime = 0.3f;
    public float decelerationRate = 10f;
    private float _timeStamp;

    [Header("Component")]
    public ScrollRect _scrollRectComponent;
    public RectTransform _scrollRectRect;
    private RectTransform _container;

    [Header("Bool")]
    private bool _lerp;
    private bool _dragging;
    private bool _showPageSelection;

    [Header("For Position")]
    private Vector2 _lerpTo;
    private List<Vector2> _pagePositions = new List<Vector2>();
    private Vector2 _startPosition;

    [Header("List<>")]

    private List<Image> _pageSelectionImages;

    //------------------------------------------------------------------------
    void Start() {
       
        _container = _scrollRectComponent.content;
        _pageCount = _container.childCount;

        _lerp = false;

        SetPagePositions();
        SetPage(startingPage);
        SetPageSelection(startingPage);        
	}

    void Update() {
        if (_lerp) {
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f) {
                _container.anchoredPosition = _lerpTo;
                _lerp = false;
                _scrollRectComponent.velocity = Vector2.zero;
            }

            if (_showPageSelection) {
                SetPageSelection(GetNearestPage());
            }
        }
    }

    private void SetPagePositions() {
        int width = 0;
        int height = 0;
        int offsetX = 0;
        int offsetY = 0;
        int containerWidth = 0;
        int containerHeight = 0;

        
        width = (int)_scrollRectRect.rect.width;
        offsetX = width / 2;
        // total width
        containerWidth = width * _pageCount;
        _fastSwipeThresholdMaxLimit = width;
        

        Vector2 newSize = new Vector2(containerWidth, containerHeight);
        _container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        _container.anchoredPosition = newPosition;

        _pagePositions.Clear();

        for (int i = 0; i < _pageCount; i++) {
            RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition;
            
                childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0f);
           
            child.anchoredPosition = childPosition;
            _pagePositions.Add(-childPosition);
        }
    }

    private void SetPage(int aPageIndex) {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _container.anchoredPosition = _pagePositions[aPageIndex];
        _currentPage = aPageIndex;
    }

    private void SwipeToPage(int aPageIndex) {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _lerpTo = _pagePositions[aPageIndex];
        _lerp = true;
        _currentPage = aPageIndex;
    }

   
    private void SetPageSelection(int aPageIndex) {
        if (_previousPageSelectionIndex == aPageIndex) {
            return;
        }
        
        if (_previousPageSelectionIndex >= 0) {
            _pageSelectionImages[_previousPageSelectionIndex].SetNativeSize();
        }

        _pageSelectionImages[aPageIndex].SetNativeSize();

        _previousPageSelectionIndex = aPageIndex;
    }

    private void NextPage() {
        SwipeToPage(_currentPage + 1);
    }

    private void PreviousPage() {
        SwipeToPage(_currentPage - 1);
    }

    private int GetNearestPage() {
        Vector2 currentPosition = _container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestPage = _currentPage;

        for (int i = 0; i < _pagePositions.Count; i++) {
            float testDist = Vector2.SqrMagnitude(currentPosition - _pagePositions[i]);
            if (testDist < distance) {
                distance = testDist;
                nearestPage = i;
            }
        }

        return nearestPage;
    }

    public void OnBeginDrag(PointerEventData aEventData) {
        _lerp = false;
        _dragging = false;
    }

    public void OnEndDrag(PointerEventData Data) {

        float difference;
        
        difference = _startPosition.x - _container.anchoredPosition.x;
        

        if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime &&
            Mathf.Abs(difference) > fastSwipeThresholdDistance &&
            Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit) {
            if (difference > 0) {
                NextPage();
            } else {
                PreviousPage();
            }
        } else {
            SwipeToPage(GetNearestPage());
        }

        _dragging = false;
    }

    public void OnDrag(PointerEventData Data) {
        if (!_dragging) {
            _dragging = true;
            _timeStamp = Time.unscaledTime;
            _startPosition = _container.anchoredPosition;
        } else {
            if (_showPageSelection) {
                SetPageSelection(GetNearestPage());
            }
        }
    }

    public void SelectScreenByOptionButton(int _MyscreenNumber)
    {
        SwipeToPage(_MyscreenNumber);
    }
    public void LoadGlobalScene()
    {
        SceneManager.LoadScene("Global");
    }

}
