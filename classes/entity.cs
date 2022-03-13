using SFML.System;
using SFML.Graphics;

namespace polygon_collision_detection {
    public class entity {
#region "Properties"
    private Vector2f position;
    public Vector2f Position => position;

    private Vector2f velocity;
    public Vector2f Velocity {
        get { return velocity; }
        set { velocity = value; }
    }

    private float angle;
    public float Angle {
        get { return angle; }
        set { angle = value; }
    }

    private float angularVelocity;
    public float AngularVelocity {
        get { return angularVelocity; }
        set { angularVelocity = value; }
    }

    private Drawable shape;
    public Drawable Shape {
        get { return shape; }
        set { shape = value; }
    }
#endregion
#region "Methods"
    public void update(float delta) {
        SetPosition(position + Velocity * delta);
        Angle += AngularVelocity * delta;
    }

    public void draw(RenderWindow window) {
        if (Shape.GetType() == typeof(VertexArray)) {
            VertexArray drawThis = new VertexArray((VertexArray)this.Shape);
            drawThis = util.rotate(drawThis, this.Angle);
            drawThis = util.transform(drawThis, this.Position);
            window.Draw(drawThis);
        }
    
        // // draw small circles on each vertex
        // for (uint i = 0; i < polygon.VertexCount; i++) {
        //     CircleShape vcs = new CircleShape(2f);
        //     vcs.Position = polygon[i].Position;
        //     vcs.Origin = new Vector2f(vcs.Radius, vcs.Radius);
        //     vcs.FillColor = Color.Red;
        //     window.Draw(vcs);
            
        //     // // draw point position
        //     // if (i < polygon.VertexCount - 1) {
        //     //     Vector2i pos = (Vector2i)polygon[i].Position;
        //     //     Text pointText = new Text(string.Format("{0}, {1}", pos.X, pos.Y), Fonts.Arial);
        //     //     pointText.Position = (Vector2f)pos + new Vector2f(4, 4);
        //     //     pointText.FillColor = Color.White;
        //     //     pointText.CharacterSize = 12;
        //     //     window.Draw(pointText);
        //     // }                
        // }

    }

    public void SetPosition(Vector2f pos) {
        position = pos;
    }

    public void SetXPosition(float x) {
        position.X = x;
    }

    public void SetYPosition(float y) {
        position.Y = y;
    }
#endregion
    }
}