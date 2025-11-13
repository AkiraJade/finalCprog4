import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

public class pAdvLevel2 extends World {

    enum PlatformState {
        WAITING, MOVING, STOPPED, REMOVED, DISAPPEAR, SPAWNED, APPEAR
    }

    enum SpikeState {
        NOT_CREATED, CREATED, READY, MOVING, STOPPED, DISAPPEAR, APPEAR
    }

    int playerX;
    int playerY;
    Platform p2Dis;
    Platform p3Dis;
    Platform p4Dis;
    PlatformTrap pt1;
    Spike sp1;
    Spike sp2;
    Spike sp3;
    Spike sp4;
    SpikeState sp1State = SpikeState.CREATED;
    SpikeState sp2State = SpikeState.CREATED;
    SpikeState sp3State = SpikeState.CREATED;
    SpikeState sp4State = SpikeState.CREATED;
    PlatformState p2DisState = PlatformState.WAITING;
    PlatformState p3DisState = PlatformState.WAITING;
    PlatformState p4DisState = PlatformState.WAITING;

    public pAdvLevel2() {
        super(750, 400, 1);
        prepare();
    }

    public void prepare() {
        Person player1 = new Person();
        addObject(player1, 30, 180);
         advPortal adv = new advPortal(5);
        addObject(adv, 70, 314);
        Platform p1 = new Platform(320, 80);
        addObject(p1, 10, 240);
        Platform p2 = new Platform(40, 10);
        addObject(p2, 230, 180);
        p2Dis = new Platform(40, 10);
        addObject(p2Dis, 270, 180);
        Platform p3 = new Platform(40, 10);
        addObject(p3, 360, 150);
        p3Dis = new Platform(40, 10);
        addObject(p3Dis, 400, 150);
        Platform p4 = new Platform(50, 10);
        addObject(p4, 490, 120);
        p4Dis = new Platform(40, 10);
        addObject(p4Dis, 530, 120);
        Platform p5 = new Platform(200, 300);
        addObject(p5, 760, 350);
        Platform p6 = new Platform(320, 70);
        addObject(p6, 10, 385);
        pt1 = new PlatformTrap();
        GreenfootImage pt1Img = pt1.getImage();
        pt1Img.scale(320, 80);
        addObject(pt1, 10, 310);
        Platform p7Invi = new Platform(20, 20);
        addObject(p7Invi, 230, 370);
        GreenfootImage p7Img = p7Invi.getImage();
        p7Img.setTransparency(0);
        Platform p7 = new Platform(80, 10);
        addObject(p7, 720, 120);
        FakeAdvPortal fp1 = new FakeAdvPortal();
        addObject(fp1, 720, 165);
       pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }

    public void act() {
        updatePlayerPosition();
        opponentPosition();
        trapsV1();
        trapsV2();
        checkPortalCollision(); // ✅ added reset check
    }

    int opponentX;
    int opponentY;

    public void updatePlayerPosition() {
        java.util.List<Person> players = getObjects(Person.class);
        if (players.isEmpty()) return;
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
    }

    public void opponentPosition() {
        java.util.List<Opponent> opponents = getObjects(Opponent.class);
        if (opponents.isEmpty()) return;
        Opponent opponent = opponents.get(0);
        opponentX = opponent.getX();
        opponentY = opponent.getY();
    }

    int frame = 0;
    int frame2 = 0;
    int frame3 = 0;
    int targetFrame = 120;
    boolean opponent = false;
    Opponent pOpponent;
    boolean shurikenThrown = false;
    Shuriken s1;

    public void trapsV1() {
        if (playerX >= 185 && playerY <= 100 && sp1State == SpikeState.CREATED) {
            sp1 = new Spike();
            addObject(sp1, 225, 167);
            sp1State = SpikeState.APPEAR;
        }

        if (playerX <= 170 && sp1State == SpikeState.APPEAR) {
            removeObject(sp1);
        }

        if (playerX >= 260 && sp1State == SpikeState.APPEAR && playerX <= 330 && p2DisState == PlatformState.WAITING) {
            if (frame < targetFrame) {
                frame++;
                if (frame == targetFrame) {
                    removeObject(p2Dis);
                    p2DisState = PlatformState.DISAPPEAR;
                }
            }
        }

        if (playerX >= 290 && playerY <= 75 && sp2State == SpikeState.CREATED && sp1State == SpikeState.APPEAR) {
            sp2 = new Spike();
            addObject(sp2, 355, 137);
            sp2State = SpikeState.APPEAR;
        }

        if (playerX <= 293 && sp2State == SpikeState.APPEAR) {
            removeObject(sp2);
        }

        if (playerX >= 395 && sp2State == SpikeState.APPEAR && playerX <= 440 && p3DisState == PlatformState.WAITING) {
            if (frame2 < targetFrame) {
                frame2++;
                if (frame2 == targetFrame) {
                    removeObject(p3Dis);
                    p3DisState = PlatformState.DISAPPEAR;
                }
            }
        }

        if (playerX >= 420 && playerY <= 50 && sp2State == SpikeState.APPEAR && sp3State == SpikeState.CREATED) {
            sp3 = new Spike();
            addObject(sp3, 480, 107);
            sp3State = SpikeState.APPEAR;
        }

        if (playerX <= 420 && sp3State == SpikeState.APPEAR) {
            removeObject(sp3);
        }

        if (playerX >= 450 && !opponent) {
            opponent = true;
            pOpponent = new Opponent();
            addObject(pOpponent, 720, 96);
        }

        if (opponent && pOpponent != null && pOpponent.getWorld() != null) {
            if (pOpponent.getX() >= 695) {
                pOpponent.setLocation(pOpponent.getX() - 1, pOpponent.getY());
                pOpponent.setMoving(true, false);
            } else {
                pOpponent.setMoving(false, false);
                // ✅ opponent throws shuriken once
                if (!shurikenThrown) {
                    s1 = new Shuriken();
                    addObject(s1, pOpponent.getX() - 30, pOpponent.getY() + 5);
                    shurikenThrown = true;
                }
            }
        }
    }

    boolean returned = false;
    boolean jump = false;
    boolean platformAppear;

    public void trapsV2() {
        // ✅ Move the shuriken when thrown
        if (s1 != null && s1.getWorld() != null) {
            if (!returned) {
                s1.setLocation(s1.getX() - 10, s1.getY());
                if (s1.getX() <= 10) {
                    returned = true;
                }
            } else {
                s1.setLocation(s1.getX() + 10, s1.getY());
                if (s1.getX() >= pOpponent.getX() - 30) {
                    returned = false;
                }
            }
        }

        if (playerX >= 560 && playerY <= 60 && !jump) {
            jump = true;
        }

        if (!jump && playerX >= 570 && !platformAppear) {
            Platform p8 = new Platform(50, 50);
            addObject(p8, 580, 190);
            platformAppear = true;
        }

        if (playerX >= 620 && playerY <= 117 && platformAppear && sp4State == SpikeState.CREATED) {
            sp4 = new Spike();
            addObject(sp4, 665, 192);
            sp4State = SpikeState.APPEAR;
        }

        if (sp4State == SpikeState.APPEAR && playerX <= 605) {
            removeObject(sp4);
        }
        
         if(playerX <= 147 && playerY >= 331){
            removeObject(pt1);
        }
    }

    // ✅ Detect touching FakeAdvPortal safely
    public void checkPortalCollision() {
        java.util.List<Person> players = getObjects(Person.class);
        if (players.isEmpty()) return;

        Person player = players.get(0);
        java.util.List<FakeAdvPortal> portals = getObjectsAt(player.getX(), player.getY(), FakeAdvPortal.class);

        if (!portals.isEmpty()) {
            resetTrapsAndRespawn(player);
        }
    }

    // ✅ Full reset logic — platform p8 resets, opponent + shuriken rethrows
    public void resetTrapsAndRespawn(Person player) {
        // Remove spikes and opponent
        if (sp1 != null && sp1.getWorld() != null) removeObject(sp1);
        if (sp2 != null && sp2.getWorld() != null) removeObject(sp2);
        if (sp3 != null && sp3.getWorld() != null) removeObject(sp3);
        if (sp4 != null && sp4.getWorld() != null) removeObject(sp4);
        if (pOpponent != null && pOpponent.getWorld() != null) removeObject(pOpponent);
        if (s1 != null && s1.getWorld() != null) removeObject(s1); // ✅ reset shuriken

        // Remove platform p8 if it exists
        java.util.List<Platform> allPlatforms = getObjects(Platform.class);
        for (Platform plat : allPlatforms) {
            if (plat.getX() == 580 && plat.getY() == 190) {
                removeObject(plat);
                break;
            }
        }

        // Reset states
        sp1State = SpikeState.CREATED;
        sp2State = SpikeState.CREATED;
        sp3State = SpikeState.CREATED;
        sp4State = SpikeState.CREATED;
        p2DisState = PlatformState.WAITING;
        p3DisState = PlatformState.WAITING;
        p4DisState = PlatformState.WAITING;

        frame = frame2 = frame3 = 0;

        opponent = false;
        shurikenThrown = false;
        returned = false;
        jump = false;
        platformAppear = false;

        // Respawn player
        player.setLocation(30, 180);

        // Recreate missing disappearing platforms
        if (p2Dis == null || p2Dis.getWorld() == null) {
            p2Dis = new Platform(40, 10);
            addObject(p2Dis, 270, 180);
        }
        if (p3Dis == null || p3Dis.getWorld() == null) {
            p3Dis = new Platform(40, 10);
            addObject(p3Dis, 400, 150);
        }
        if (p4Dis == null || p4Dis.getWorld() == null) {
            p4Dis = new Platform(40, 10);
            addObject(p4Dis, 530, 120);
        }

       
    }
}