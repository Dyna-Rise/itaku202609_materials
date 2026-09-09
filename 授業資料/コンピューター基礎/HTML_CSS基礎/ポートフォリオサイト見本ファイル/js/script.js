document.addEventListener('DOMContentLoaded', () => {
    
    // 1. スムーススクロールの設定 (ヘッダーのリンク用)
    const smoothScrollLinks = document.querySelectorAll('a[href^="#"]');

    smoothScrollLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            // href が "#" だけの場合は何もしない
            if (link.getAttribute('href') === '#') return;

            e.preventDefault();
            const targetId = link.getAttribute('href');
            const targetElement = document.querySelector(targetId);

            if (targetElement) {
                // ヘッダーの高さを取得して、隠れないように調整
                const headerHeight = document.querySelector('header').offsetHeight;
                const elementPosition = targetElement.getBoundingClientRect().top;
                const offsetPosition = elementPosition + window.pageYOffset - headerHeight;

                window.scrollTo({
                    top: offsetPosition,
                    behavior: 'smooth'
                });
            }
        });
    });

    // 2. フェードインアニメーションの設定 (Intersection Observer API)
    const fadeElements = document.querySelectorAll('.fade-in');

    const fadeInOptions = {
        threshold: 0.1, // 要素が画面に10%入ったら発火
        rootMargin: '0px 0px -50px 0px' // 発火タイミングを少し早める
    };

    const fadeInObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // ターゲット要素に 'appear' クラスを追加してアニメーション発動
                entry.target.classList.add('appear');
                // 一度表示されたら監視を解除（パフォーマンスのため）
                observer.unobserve(entry.target);
            }
        });
    }, fadeInOptions);

    // ページ読み込み時に画面内にある要素も監視対象にする
    fadeElements.forEach(element => {
        fadeInObserver.observe(element);
    });

    // 3. ボタンのホバーエフェクト (追加の華やかさ)
    const githubButtons = document.querySelectorAll('.btn-github');

    githubButtons.forEach(button => {
        button.addEventListener('mouseover', () => {
            button.style.transform = 'translateY(-2px)';
            button.style.boxShadow = '0 5px 15px rgba(0, 0, 0, 0.3)';
        });

        button.addEventListener('mouseout', () => {
            button.style.transform = 'translateY(0)';
            button.style.boxShadow = 'none';
        });
    });
});