using System;
using System.Collections.Generic;
using System.Linq;
using Android.Animation;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Java.Lang;
using static System.Math;
using RPN;

namespace CalcApp
{
    [Activity(MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private HorizontalScrollView _hsv;
        private HorizontalScrollView _hsvSec;
        private TextView _primaryDisplay;
        private TextView _secondaryDisplay;
        private string _expr;
        private Calculator _calc;
        private ValueAnimator _valueAnimator;
        private LinearLayout _displayOverlay;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            _calc = new Calculator();
            _hsv = FindViewById<HorizontalScrollView>(Resource.Id.display_hsv);
            _hsvSec = FindViewById<HorizontalScrollView>(Resource.Id.display_hsv_sec);
            _primaryDisplay = FindViewById<TextView>(Resource.Id.display_primary);
            _secondaryDisplay = FindViewById<TextView>(Resource.Id.display_secondary);
            _displayOverlay = FindViewById<LinearLayout>(Resource.Id.display_overlay);

            var del = FindViewById<LinearLayout>(Resource.Id.button_delete);
            var eq = FindViewById<TextView>(Resource.Id.button_equals);

            _hsv.ViewTreeObserver.GlobalLayout += (sender, args) =>
            {
                _hsv.FullScroll(FocusSearchDirection.Right);
            };

            _hsvSec.ViewTreeObserver.GlobalLayout += (sender, args) =>
            {
                _hsvSec.FullScroll(FocusSearchDirection.Right);
            };

            del.LongClick += (sender, args) => CircleAnimation();

            eq.Click += (sender, args) =>
            {
                _primaryDisplay.Animate().Alpha(0).SetDuration(200).WithEndAction(new Runnable(() =>
                {
                    _primaryDisplay.Text = string.Empty;
                    _primaryDisplay.Alpha = 1;
                })).Start();

                float startSize = 34;
                float endSize = 70;

                _valueAnimator = ValueAnimator.OfFloat(startSize, endSize);
                _valueAnimator.SetInterpolator(new DecelerateInterpolator());
                _valueAnimator.SetDuration(200);

                _valueAnimator.Update += (o, eventArgs) =>
                {
                    _secondaryDisplay.TextSize = (float)_valueAnimator.AnimatedValue;
                };

                _valueAnimator.Start();
            };

            del.Click += (sender, args) =>
            {
                if (_primaryDisplay.Text.Length != 0)
                {
                    _primaryDisplay.Text = _primaryDisplay.Text.Remove(_primaryDisplay.Text.Length - 1);
                    if (_primaryDisplay.Text.Length != 0)
                    {
                        if (!IsOperator(_primaryDisplay.Text[_primaryDisplay.Text.Length - 1]))
                            Calculate();
                    }
                    else
                        _secondaryDisplay.Text = string.Empty;
                }
            };

            GetDigits().ForEach(x => x.Click += (sender, args) =>
            {
                _primaryDisplay.Text += x.Text;
                Calculate();
            });

            GetOperators().ForEach(x => x.Click += (sender, args) =>
            {
                if (x.Text == "." && _primaryDisplay.Text.Contains(".")) return;
                if (_primaryDisplay.Text.Length != 0)
                    if (IsOperator(_primaryDisplay.Text[_primaryDisplay.Text.Length - 1]) && x.Text != "−")
                        _primaryDisplay.Text = _primaryDisplay.Text.Remove(_primaryDisplay.Text.Length - 1) + x.Text;
                    else if(_primaryDisplay.Text[_primaryDisplay.Text.Length - 1] != '−')
                        _primaryDisplay.Text += x.Text;
                        else return;
                else
                    _primaryDisplay.Text = x.Text;
            });

            GetFunctions().ForEach(x => x.Click += (sender, args) =>
            {
                _primaryDisplay.Text += x.Text;
                if (x.Text == ")")
                    Calculate();
            });
        }

        private void CircleAnimation()
        {
            if (_primaryDisplay.Text.Length != 0)
            {
                var circle = ViewAnimationUtils.CreateCircularReveal(
                    _displayOverlay,
                    _displayOverlay.MeasuredWidth / 2,
                    _displayOverlay.MeasuredHeight,
                     0,
                    (int)Hypot(_displayOverlay.Width, _displayOverlay.Height));

                circle.SetDuration(300);

                circle.AnimationEnd += (o, eventArgs) =>
                {
                    _primaryDisplay.Text = string.Empty;
                    _secondaryDisplay.Text = string.Empty;
                };

                var fade = ObjectAnimator.OfFloat(_displayOverlay, "alpha", 0f);
                fade.SetInterpolator(new DecelerateInterpolator());
                fade.SetDuration(200);
                var animatorSet = new AnimatorSet();
                animatorSet.PlaySequentially(circle, fade);
                _displayOverlay.Alpha = 1;
                animatorSet.Start();
            }
        }

        private static double Hypot(double leftOp, double rightOp)
        {
            return Sqrt(Pow(leftOp, 2) + Pow(rightOp, 2));
        }

        private void Calculate()
        {
            _secondaryDisplay.TextSize = 34;

            _expr = _primaryDisplay.Text.Replace('√', '#')
                                        .Replace('÷', '/')
                                        .Replace('−', '-')
                                        .Replace('×', '*');
            try
            {
                _secondaryDisplay.Text = _calc.Calculate(_expr).ToString();
            }
            catch (DivideByZeroException)
            {
                _secondaryDisplay.Text = "Error! Devide by zero";
            }
            catch (OverflowException)
            {
                _secondaryDisplay.Text = "Infinity";
            }
            catch (System.Exception e)
            {
#if DEBUG
                Console.WriteLine(e);
#endif
                _secondaryDisplay.Text = "Invalid input!";
            }
        }

        private bool IsOperator(char op)
        {
            return GetOperators().Any(x => x.Text[0] == op);
        }

        private List<TextView> GetDigits()
        {
            return new List<TextView>
            {
                FindViewById<TextView>(Resource.Id.button_0),
                FindViewById<TextView>(Resource.Id.button_1),
                FindViewById<TextView>(Resource.Id.button_2),
                FindViewById<TextView>(Resource.Id.button_3),
                FindViewById<TextView>(Resource.Id.button_4),
                FindViewById<TextView>(Resource.Id.button_5),
                FindViewById<TextView>(Resource.Id.button_6),
                FindViewById<TextView>(Resource.Id.button_7),
                FindViewById<TextView>(Resource.Id.button_8),
                FindViewById<TextView>(Resource.Id.button_9),
            };
        }

        private List<TextView> GetOperators()
        {
            return new List<TextView>
            {
                FindViewById<TextView>(Resource.Id.button_decimal),
                FindViewById<TextView>(Resource.Id.button_divide),
                FindViewById<TextView>(Resource.Id.button_add),
                FindViewById<TextView>(Resource.Id.button_multiply),
                FindViewById<TextView>(Resource.Id.button_subtract),
            };
        }

        private List<TextView> GetFunctions()
        {
            return new List<TextView>
            {
                FindViewById<TextView>(Resource.Id.button_square_root),
                FindViewById<TextView>(Resource.Id.button_deg),
                FindViewById<TextView>(Resource.Id.button_start_parenthesis),
                FindViewById<TextView>(Resource.Id.button_end_parenthesis)
            };
        }
    }
}
