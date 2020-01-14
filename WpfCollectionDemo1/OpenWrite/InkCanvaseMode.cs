using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;

namespace OpenWrite
{
    public class InkCanvaseMode : InkCanvas
    {

        public InkCanvaseMode()
        {
            EraserShape = new RectangleStylusShape(50, 70);
        }


        IncrementalStrokeHitTester eraseTester;

        // Collect the StylusPackets as the stylus moves.
        protected override void OnStylusMove(StylusEventArgs e)
        {
            if (eraseTester.IsValid)
            {
                eraseTester.AddPoints(e.GetStylusPoints(this));
            }
        }

        // Unsubscribe from the StrokeHitChanged event when the
        // user lifts the stylus.
        protected override void OnStylusUp(StylusEventArgs e)
        {
            eraseTester.AddPoints(e.GetStylusPoints(this));
            eraseTester.StrokeHit -= new
                StrokeHitEventHandler(eraseTester_StrokeHit);
            eraseTester.EndHitTesting();
        }


        // When the stylus intersects a stroke, erase that part of
        // the stroke.  When the stylus dissects a stoke, the 
        // Stroke.Erase method returns a StrokeCollection that contains
        // the two new strokes.
        void eraseTester_StrokeHit(object sender,
            StrokeHitEventArgs args)
        {
            StrokeCollection eraseResult =
                args.GetPointEraseResults();
            StrokeCollection strokesToReplace = new StrokeCollection();
            strokesToReplace.Add(args.HitStroke);

            // Replace the old stroke with the new one.
            if (eraseResult.Count > 0)
            {
                this.Strokes.Replace(strokesToReplace, eraseResult);
            }
            else
            {
                this.Strokes.Remove(strokesToReplace);
            }


        }

    }
}
