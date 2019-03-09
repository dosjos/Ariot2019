using System.Threading;
using DiggerSoft.Servo;

namespace DiggerSoft
{
    public class CamServo
    {
        private readonly ServoMotor _servoMotor;

        public CamServo()
        {
            _servoMotor = new ServoMotor(21, -1, new ServoMotorDefinition(900, 2100));
        }

        private readonly uint[] _pulsArray = { 1200, 2100, 1650 };
        private int _currentPulsArrayPosition = 0;
        public void Turn()
        {
            if (_currentPulsArrayPosition >= _pulsArray.Length)
            {
                _currentPulsArrayPosition = 0;
            }
            SetPuls(_pulsArray[_currentPulsArrayPosition], 2000);
        }

        public void SetPuls(uint puls, int sleep)
        {
            _servoMotor.SetPulse(puls);
            Thread.Sleep(1000);
            _servoMotor.SetPulse(0);
            Thread.Sleep(sleep);
        }

        public void SetNextPosition()
        {
            if (_currentPulsArrayPosition >= _pulsArray.Length)
            {
                _currentPulsArrayPosition = 0;
            }
            else
            {
                _currentPulsArrayPosition++;
            }

        }

        public void SetPosition(Position pos)
        {
            if (pos == Position.Left)
            {
                _currentPulsArrayPosition = 0;
            }
            else if (pos == Position.Right)
            {
                _currentPulsArrayPosition = 1;
            }
            else
            {
                _currentPulsArrayPosition = 2;
            }
        }

        public Position GetCurrentPosition()
        {
            if (_currentPulsArrayPosition == 0)
            {
                return Position.Left;
            }
            else if (_currentPulsArrayPosition == 1)
            {
                return Position.Right;
            }
            else
            {
                return Position.Center;
            }
        }
    }

    public enum Position
    {
        Left,
        Right,
        Center
    }
}
