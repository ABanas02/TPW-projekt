﻿using Logika;
using Logika.API;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class ModelAPI
    {

        public static ModelAPI CreateApi(LogikaAPI logikaAPI)
        {
            if (logikaAPI == null)
            {
                return new ModelAPIBase(LogikaAPI.CreateApi(null));
            }
            else
            {
                return new ModelAPIBase(logikaAPI);
            }
            
        }

        public abstract void CreateBall();

        public abstract ObservableCollection<object> GetObjects();

        internal class ModelAPIBase : ModelAPI
        {
            private LogikaAPI _logikaAPI;
            public ModelAPIBase(int boardWidth, int boardHeight)
            {
                _logikaAPI = LogikaAPI.CreateApi();
            }

            public override void Start()
            {
                _logikaAPI.Start();
            }

            public override void Stop()
            {
                _logikaAPI = logikaAPI;  
            }

            public override void CreateBall()
            {
                _logikaAPI.CreateBall();
            }

            public override ObservableCollection<object> GetObjects()
            {
                ObservableCollection<object> objects = new ObservableCollection<object>();
                foreach (var ball in _logikaAPI.balls)
                    objects.Add(ball);
                return objects;
            }
        }
    }
}
