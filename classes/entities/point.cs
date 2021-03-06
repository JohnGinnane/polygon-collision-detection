using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class point : body {
        private Color colour;
        public Color Colour {
            get { return colour; }
            set { colour = value; }
        }

        public point() {
            bodytype = enumBodyType.point;
            colour = Color.White;
        }

        public override void draw(RenderWindow window)
        {
            CircleShape cs = new CircleShape(2f);
            cs.Origin = new Vector2f(2f, 2f);
            cs.Position = Position;
            cs.FillColor = Colour;
            cs.OutlineColor = Color.Transparent;
            cs.OutlineThickness = 0;

            window.Draw(cs);

            base.draw(window);
        }
    }
}