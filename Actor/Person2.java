import greenfoot.*;

public class Person2 extends Actor
{
    private GreenfootImage[] idleFrames;
    private GreenfootImage[] runFrames;
    private GreenfootImage[] jumpFrames;
    private int frameIndex = 0;
    private int delayCounter = 0;
    private int animationDelay = 5;
    private boolean facingRight = true;
    private boolean isRunning = false;

    private boolean onGround = false;
    private int jumpStrength = -15;
    private int gravity = 1;
    private int vSpeed = 0;
    private int moveSpeed = 3;
    private int airSpeed = 2;

    private Platform lastPlatform = null;
    private int lastPlatformX = 0;
    private int lastPlatformY = 0;

    public Person2() {
        idleFrames = new GreenfootImage[2];
        for (int i = 0; i < idleFrames.length; i++) {
            idleFrames[i] = new GreenfootImage("idleHood (" + (i + 1) + ").png");
        }

        runFrames = new GreenfootImage[8];
        for (int i = 0; i < runFrames.length; i++) {
            runFrames[i] = new GreenfootImage("runHood (" + (i + 1) + ").png");
        }

        jumpFrames = new GreenfootImage[9];
        for (int i = 0; i < jumpFrames.length; i++) {
            jumpFrames[i] = new GreenfootImage("jumpHood (" + (i + 1) + ").png");
        }

        setImage(idleFrames[0]);
    }

    public void act() {
        checkPlatformPush();
        handleMovement();
        correctPlatformPenetration();
        ensureFullyOutsidePlatforms();
        applyGravity();
        checkSpikesAndTraps();
        checkFallOff();
        nextLevel();
        animate();
    }

    private void ensureFullyOutsidePlatforms() {
        if (onGround) return;
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);

        for (Platform platform : platforms) {
            if (pixelPerfectCollision(platform.getImage(), platform)) {
                int personLeft = getX() - getImage().getWidth() / 2;
                int personRight = getX() + getImage().getWidth() / 2;
                int personTop = getY() - getImage().getHeight() / 2;
                int personBottom = getY() + getImage().getHeight() / 2;

                int platformLeft = platform.getX() - platform.getImage().getWidth() / 2;
                int platformRight = platform.getX() + platform.getImage().getWidth() / 2;
                int platformTop = platform.getY() - platform.getImage().getHeight() / 2;
                int platformBottom = platform.getY() + platform.getImage().getHeight() / 2;

                int penetrationLeft = personRight - platformLeft;
                int penetrationRight = platformRight - personLeft;
                int penetrationTop = personBottom - platformTop;
                int penetrationBottom = platformBottom - personTop;

                int minPenetration = Math.min(Math.min(penetrationLeft, penetrationRight),
                                              Math.min(penetrationTop, penetrationBottom));

                if (minPenetration <= 3) continue;

                if (minPenetration == penetrationLeft && penetrationLeft > 0)
                    setLocation(platformLeft - getImage().getWidth() / 2, getY());
                else if (minPenetration == penetrationRight && penetrationRight > 0)
                    setLocation(platformRight + getImage().getWidth() / 2, getY());
                else if (minPenetration == penetrationTop && penetrationTop > 0)
                    setLocation(getX(), platformTop - getImage().getHeight() / 2);
                else if (minPenetration == penetrationBottom && penetrationBottom > 0) {
                    setLocation(getX(), platformBottom + getImage().getHeight() / 2);
                    vSpeed = 0;
                }
            }
        }
    }

    private void checkPlatformPush() {
        Platform currentPlatform = (Platform) getOneIntersectingObject(Platform.class);

        if (currentPlatform != null && onGround) {
            if (lastPlatform == currentPlatform) {
                int dx = currentPlatform.getX() - lastPlatformX;
                int dy = currentPlatform.getY() - lastPlatformY;
                if (dx != 0 || dy != 0)
                    setLocation(getX() + dx, getY() + dy);
            }
            lastPlatform = currentPlatform;
            lastPlatformX = currentPlatform.getX();
            lastPlatformY = currentPlatform.getY();
        } else lastPlatform = null;
    }

    private void correctPlatformPenetration() {
        if (!isRunning || !onGround) return;
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);

        for (Platform platform : platforms) {
            if (pixelPerfectCollision(platform.getImage(), platform)) {
                setLocation(getX() - (facingRight ? 1 : -1), getY());
            }
        }
    }

    private void handleMovement() {
        isRunning = false;
        int speed = onGround ? moveSpeed : airSpeed;

        // ✅ Correct movement direction depending on world type
        if (getWorld() instanceof mediumLevel1 || getWorld() instanceof mediumLevel5) {
            if (Greenfoot.isKeyDown("a")) { facingRight = true; isRunning = true; setLocation(getX() + speed, getY()); }
            else if (Greenfoot.isKeyDown("d")) { facingRight = false; isRunning = true; setLocation(getX() - speed, getY()); }
        } else {
            if (Greenfoot.isKeyDown("a")) { facingRight = false; isRunning = true; setLocation(getX() - speed, getY()); }
            else if (Greenfoot.isKeyDown("d")) { facingRight = true; isRunning = true; setLocation(getX() + speed, getY()); }
        }

        if ((Greenfoot.isKeyDown("space") || Greenfoot.isKeyDown("w")) && onGround) jump();
    }

    private void jump() {
        vSpeed = jumpStrength;
        onGround = false;
    }

    private void applyGravity() {
        if (vSpeed < 9) vSpeed += gravity;
        int steps = Math.abs(vSpeed);
        int sign = vSpeed > 0 ? 1 : -1;

        for (int i = 0; i < steps; i++) {
            setLocation(getX(), getY() + sign);

            if (isTouchingPlatform()) {
                setLocation(getX(), getY() - sign);

                if (vSpeed > 0) onGround = true;

                vSpeed = 0;
                return;
            }
        }
        onGround = false;
    }

    private boolean isTouchingPlatform() {
        java.util.List<Platform> platforms = getIntersectingObjects(Platform.class);
        for (Platform p : platforms)
            if (pixelPerfectCollision(p.getImage(), p)) return true;
        return false;
    }

    private boolean pixelPerfectCollision(GreenfootImage otherImg, Actor other) {
        GreenfootImage img = getImage();
        int dx = getX() - other.getX();
        int dy = getY() - other.getY();

        for (int x = 0; x < img.getWidth(); x++)
            for (int y = 0; y < img.getHeight(); y++) {
                int ox = x - img.getWidth() / 2 - dx + otherImg.getWidth() / 2;
                int oy = y - img.getHeight() / 2 - dy + otherImg.getHeight() / 2;

                if (ox >= 0 && ox < otherImg.getWidth() && oy >= 0 && oy < otherImg.getHeight())
                    if (img.getColorAt(x, y).getAlpha() > 0 && otherImg.getColorAt(ox, oy).getAlpha() > 0)
                        return true;
            }
        return false;
    }

    private void checkSpikesAndTraps() {
        if (getOneIntersectingObject(Spike.class) != null)
            Greenfoot.setWorld(new easyLevel1());
    }

    private void checkFallOff() {
        if (getY() >= 399)
            Greenfoot.setWorld(new easyLevel1());
    }

    public void nextLevel() {
        World w = getWorld();

        if (getOneIntersectingObject(easyPortal.class) != null) {
            if (w instanceof easyLevel1) Greenfoot.setWorld(new easyLevel2());
            else if (w instanceof easyLevel2) Greenfoot.setWorld(new easyLevel3());
            else if (w instanceof easyLevel3) Greenfoot.setWorld(new easyLevel4());
            else if (w instanceof easyLevel4) Greenfoot.setWorld(new easyLevel5());
            else if (w instanceof easyLevel5) Greenfoot.setWorld(new categoryEasy());
        }
    }

    private void animate() {
        delayCounter++;
        if (delayCounter >= animationDelay) {
            delayCounter = 0;
            frameIndex++;

            GreenfootImage[] set = !onGround ? jumpFrames :
                                   (isRunning ? runFrames : idleFrames);

            if (frameIndex >= set.length) frameIndex = 0;
            GreenfootImage img = new GreenfootImage(set[frameIndex]);

            if (!facingRight) img.mirrorHorizontally();
            setImage(img);
        }
    }
}
